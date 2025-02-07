import dayjs from "dayjs";
import { useState } from "react";

import styles from "./AddTaskForm.module.less";
import Button from "../../UI/Button/Button";
import Field from "../../UI/Field/Field";
import TextArea from "../../UI/TextArea/TextArea";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import axios from "axios";

const AddTaskForm = (props) => {
  const [inputDescription, setInputDescription] = useState("");
  const [inputDate, setInputDate] = useState(dayjs());

  const addTaskHandler = async (e) => {
    e.preventDefault();
    console.log(props.projectId);
    const dtoTask = (
      await axios.post(`api/v1/ProjectTask`, {
        deadline: inputDate.toString(),
        description: inputDescription,
        projectId: props.projectId,
      })
    ).data;

    props.closeModalHandler();
    props.handleTask();
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
      onSubmit={addTaskHandler}
    >
      <div className={styles.form__wrapper}>
        <Button
          className={styles.form__close}
          onClick={onClickHandler}
          type="close"
        ></Button>
        <div className={styles.form__container}>
          <p className={styles.form__title}>Создание задачи...</p>

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
            Создать
          </Button>
        </div>
      </div>
    </form>
  );
};

export default AddTaskForm;
