import { useState } from "react";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import BinButton from "../../UI/BinButton/BinButton";
import Check from "../../UI/Check/Check";
import styles from "./TaskCard.module.less";
import "flatpickr/dist/themes/material_green.css";
import dayjs from "dayjs";
import axios from "axios";
import Svg from "../../UI/Svg/Svg";
import Modal from "../../UI/Modal/Modal";
import UpdateTaskForm from "../UpdateTaskForm/UpdateTaskForm";

const TaskCard = (props) => {
  const [date, setDate] = useState(dayjs(props.deadline));
  const [isDone, setIsDone] = useState(props.isDone);

  const [isSettings, setIsSettings] = useState(false);

  const openModalHandler = () => {
    setIsSettings(true);
  };
  const closeModalHandler = () => {
    setIsSettings(false);
  };

  const onChangeTaskHandler = async (e) => {
    setIsDone(e.target.checked);

    const updatedTask = (
      await axios.put(`api/v1/ProjectTask`, {
        id: props.id,
        deadline: date.format("YYYY.MM.DD").toString(),
        description: props.description,
        isDone: e.target.checked,
      })
    ).data.data;

    props.onTaksIsDoneHandler(props.id, e.target.checked);
  };
  const onChnageDateHandler = (date) => {
    setDate(dayjs(date));
  };
  const handleDeleteTask = async () => {
    const deletedTask = (await axios.delete(`api/v1/ProjectTask/${props.id}`))
      .data.data;

    console.log(deletedTask);

    props.handleDeleteTask(props.id);
  };
  const updateTaskHandler = (task) => {
    setDate(dayjs(task.deadline));
    props.updateTaskHandler();
  };
  return (
    <>
      {isSettings && (
        <Modal>
          <UpdateTaskForm
            updateTaskHandler={updateTaskHandler}
            closeModalHandler={closeModalHandler}
            task={{
              id: props.id,
              date: date,
              isDone: isDone,
              description: props.description,
            }}
          />
        </Modal>
      )}
      <div className={styles["task-card"]}>
        <Check
          id={props.id}
          isDone={isDone}
          className={styles["task-card__label"]}
          onChangeHandler={onChangeTaskHandler}
        >
          {props.description}
        </Check>
        <Svg
          onClick={openModalHandler}
          className={styles["task-card__settings"]}
          width="32"
          height="32"
          href="/src/assets/settings.svg"
          svgId="settings"
        />
        <div
          className={`${styles["task-card__datepicker-container"]} ${
            !props.isDone &&
            dayjs(date).diff(dayjs(), "day") < 0 &&
            styles["task-card__datepicker-container--dead"]
          }`}
        >
          <LocalizationProvider dateAdapter={AdapterDayjs}>
            <DatePicker
              disabled
              className={styles["task-card__datepicker"]}
              value={date}
              onAccept={onChnageDateHandler}
              format="DD.MM.YYYY"
              sx={{
                "& .MuiOutlinedInput-root": {
                  "& .Mui-disabled": {
                    color: "#676767",
                    "-webkit-text-fill-color": "#676767",
                  },
                },
              }}
            />
          </LocalizationProvider>
        </div>

        <BinButton onClick={handleDeleteTask} />
      </div>
    </>
  );
};

export default TaskCard;
