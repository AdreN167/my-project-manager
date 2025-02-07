import { DatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import Button from "../../UI/Button/Button";
import styles from "./UpdateTaskForm.module.less";
import TextArea from "../../UI/TextArea/TextArea";
import { useState } from "react";
import dayjs from "dayjs";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import axios from "axios";

const UpdateTaskForm = (props) => {
  const [inputDescription, setInputDescription] = useState(
    props.task.description
  );
  const [inputDate, setInputDate] = useState(props.task.date);

  const updateTaskHandler = async (e) => {
    e.preventDefault();

    console.log(dayjs(inputDate).format("DD.MM.YYYY").toString());
    console.log(inputDescription);
    console.log(props.task.id);
    console.log(props.task.isDone);
    const dtoTask = (
      await axios.put(`api/v1/ProjectTask`, {
        id: props.task.id,
        deadline: dayjs(inputDate).format("YYYY.MM.DD").toString(),
        description: inputDescription,
        isDone: props.task.isDone,
      })
    ).data.data;

    props.closeModalHandler();
    props.updateTaskHandler(dtoTask);
  };

  const onClickHandler = () => {
    props.closeModalHandler();
  };

  const changeInputDescriptionHandler = (e) => {
    setInputDescription(e.target.value);
  };

  const changeDateHandler = (date) => {
    setInputDate(date);
  };

  return (
    <form
      className={`${styles.form} ${props.className}`}
      onSubmit={updateTaskHandler}
    >
      <div className={styles.form__wrapper}>
        <Button
          className={styles.form__close}
          onClick={onClickHandler}
          type="close"
        ></Button>
        <div className={styles.form__container}>
          <p className={styles.form__title}>Изменение задачи...</p>

          <p className={styles.form__label}>Сроки выполнения</p>
          <div className={styles["form__datepicker-container"]}>
            <LocalizationProvider dateAdapter={AdapterDayjs}>
              <DatePicker
                sx={{ "& .MuiOutlinedInput-root": { borderRadius: "10px" } }}
                className={styles["form__datepicker"]}
                value={inputDate}
                onChange={changeDateHandler}
                format="DD.MM.YYYY"
              />
            </LocalizationProvider>
          </div>
          <TextArea
            isRequired={true}
            placeholder="Описание..."
            className={styles.form__textarea}
            name="description"
            value={inputDescription}
            onChange={changeInputDescriptionHandler}
          >
            Краткое описане
          </TextArea>
          <Button className={styles.form__submit} type="submit">
            Применить
          </Button>
        </div>
      </div>
    </form>
  );
};

export default UpdateTaskForm;
