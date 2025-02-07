import { TextField } from "@mui/material";
import styles from "./SignUp.module.less";
import Submit from "../../../../UI/submit/Submit";
import useInput from "../../../../../hooks/useInput";
import axios from "axios";
import { useState } from "react";

const validateEmail = (email) => {
  let re =
    /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  if (re.test(email)) return true;
  return false;
};

const SignUp = ({ onAuth }) => {
  const {
    value: email,
    hasError: emailHasError,
    isValid: isEmailValid,
    changeInputHandler: changeEmailHandler,
    blurInputHandler: wasEmailTouchedHandler,
    resetValues: resetEmailValues,
  } = useInput(validateEmail);
  const {
    value: password,
    hasError: passwordHasError,
    isValid: isPasswordValid,
    changeInputHandler: changePasswordHandler,
    blurInputHandler: wasPasswordTouchedHandler,
    resetValues: resetPasswordValues,
  } = useInput((value) => value.length >= 8);
  const {
    value: confirmPassword,
    hasError: confirmPasswordHasError,
    isValid: isConfirmPasswordValid,
    changeInputHandler: changeConfirmPasswordHandler,
    blurInputHandler: wasConfirmPasswordTouchedHandler,
    resetValues: resetConfirmPasswordValues,
  } = useInput((value) => value === password);

  const [requestError, setRequestError] = useState("");

  // проверка на валидность всей формы (значение будет обновляться каждый раз при перезагрузке компонента)
  const isFormValid = isEmailValid && isPasswordValid && isConfirmPasswordValid;

  const formSubmitHandler = async (e) => {
    e.preventDefault();
    if (!isEmailValid || !isPasswordValid || !isConfirmPasswordValid) return;

    try {
      const body = {
        login: email,
        password: password,
        passwordConfirm: confirmPassword,
      };
      await axios.post(`api/v1/Auth/register`, body);
      setRequestError("");
      resetEmailValues();
      resetPasswordValues();
      resetConfirmPasswordValues();

      const bodyForLogin = {
        login: email,
        password: password,
      };
      const { data } = await axios.post(`api/v1/Auth/login`, bodyForLogin);
      localStorage.setItem("token", data.data.accessToken);

      onAuth();
    } catch (err) {
      const { data } = err.response;
      if (data.errorCode === 22)
        setRequestError("Пользователь с таким email уже существует");
    }
  };

  return (
    <form className={styles.up} onSubmit={formSubmitHandler}>
      <div className={styles.up__field}>
        <label className={styles.up__label}>Электронная почта</label>
        <TextField
          sx={{ "& .MuiOutlinedInput-root": { borderRadius: "10px" } }}
          className={styles.up__input}
          placeholder={"Email"}
          value={email}
          onChange={changeEmailHandler}
          onBlur={wasEmailTouchedHandler}
          error={emailHasError}
          helperText={emailHasError && "Некорректный email"}
        />
      </div>
      <div className={styles.up__field}>
        <label className={styles.up__label}>Пароль</label>
        <TextField
          sx={{ "& .MuiOutlinedInput-root": { borderRadius: "10px" } }}
          className={styles.up__input}
          placeholder={"Пароль"}
          type="password"
          value={password}
          onChange={changePasswordHandler}
          onBlur={wasPasswordTouchedHandler}
          error={passwordHasError}
          helperText={
            passwordHasError && "Пароль должен содержать минимум 8 символов"
          }
        />
      </div>
      <div className={styles.up__field}>
        <label className={styles.up__label}>Повтор пароля</label>
        <TextField
          sx={{ "& .MuiOutlinedInput-root": { borderRadius: "10px" } }}
          className={styles.up__input}
          placeholder={"Повтор пароля"}
          type="password"
          value={confirmPassword}
          onChange={changeConfirmPasswordHandler}
          onBlur={wasConfirmPasswordTouchedHandler}
          error={confirmPasswordHasError}
          helperText={confirmPasswordHasError && "Пароли должны совпадать"}
        />
      </div>
      {requestError !== "" && <p className={styles.up__fail}>{requestError}</p>}
      <Submit disabled={isFormValid} className={styles.up__submit}>
        Зарегистрироваться
      </Submit>
      <p className={styles.up__policy}>
        Регистрируясь, я принимаю условия Политики конфиденциальности и
        Пользовательского соглашения
      </p>
    </form>
  );
};

export default SignUp;
