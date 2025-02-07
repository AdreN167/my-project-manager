import { TextField } from "@mui/material";
import styles from "./SignIn.module.less";
import Submit from "../../../../UI/submit/Submit";
import useInput from "../../../../../hooks/useInput";
import axios from "axios";
import { useState } from "react";

const SignIn = ({ onAuth }) => {
  const {
    value: email,
    hasError: emailHasError,
    isValid: isEmailValid,
    changeInputHandler: changeEmailHandler,
    blurInputHandler: wasEmailTouchedHandler,
    resetValues: resetEmailValues,
  } = useInput((value) => value.trim() !== "");

  const {
    value: password,
    hasError: passwordHasError,
    isValid: isPasswordValid,
    changeInputHandler: changePasswordHandler,
    blurInputHandler: wasPasswordTouchedHandler,
    resetValues: resetPasswordValues,
  } = useInput((value) => value.trim() !== "");

  const [requestError, setRequestError] = useState("");

  // проверка на валидность всей формы (значение будет обновляться каждый раз при перезагрузке компонента)
  const isFormValid = isEmailValid && isPasswordValid;

  const formSubmitHandler = async (e) => {
    e.preventDefault();
    if (!isEmailValid || !isPasswordValid) return;

    try {
      const body = {
        login: email,
        password: password,
      };
      const { data } = await axios.post(`api/v1/Auth/login`, body);

      localStorage.setItem("token", data.data.accessToken);
      localStorage.setItem("login", email);

      setRequestError("");
      resetEmailValues();
      resetPasswordValues();
      onAuth();
    } catch (err) {
      const { data } = err.response;
      if (data.errorCode === 21)
        setRequestError("Пользователь с таким email не зарегистрирован");
      else if (data.errorCode === 32) setRequestError("Неверный пароль");
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
          helperText={emailHasError && "Это поле не может быть пустым"}
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
          helperText={passwordHasError && "Это поле не может быть пустым"}
        />
      </div>
      {requestError !== "" && <p className={styles.up__fail}>{requestError}</p>}
      <Submit disabled={isFormValid} className={styles.up__submit}>
        Войти
      </Submit>
    </form>
  );
};

export default SignIn;
