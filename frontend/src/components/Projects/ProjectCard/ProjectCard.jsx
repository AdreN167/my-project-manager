import styles from "./ProjectCard.module.less";
import Button from "../../UI/Button/Button";
import { useState } from "react";
import ProjectDescription from "../ProjectDescription/ProjectDescription";
import BinButton from "../../UI/BinButton/BinButton";
import ProgressBar from "../../UI/ProgressBar/ProgressBar";
import axios from "axios";
import Svg from "../../UI/Svg/Svg";
import Modal from "../../UI/Modal/Modal";
import UpdateProjectForm from "../UpdateProjectForm/UpdateProjectForm";

const ProjectCard = (props) => {
  const [doneTasksCount, setDoneTasksCount] = useState(
    props.tasks.filter((task) => task.isDone === true).length
  );
  const [isProjectOpened, setIsProjectOpened] = useState(false);

  const [isSettings, setIsSettings] = useState(false);

  const openProjectHandler = () => {
    setIsProjectOpened((prev) => !prev);
  };
  const onTaksIsDoneHandler = (id, value) => {
    props.tasks.filter((task) => {
      if (task.id === id) {
        task.isDone = value;
      }
    });
    console.log(id);
    setDoneTasksCount(
      props.tasks.filter((task) => task.isDone === true).length
    );
  };
  const handleDeleteProject = async () => {
    await axios.delete(`api/v1/Project/${props.projectId}`);
    props.handleDeleteProject(props.title);
  };

  const handleDeleteTask = (id) => {
    props.handleDeleteTask(props.title, id);
    setDoneTasksCount(
      props.tasks.filter((task) => task.isDone === true && task.id !== id)
        .length
    );
    console.log(doneTasksCount);
  };

  const openModalHandler = () => {
    setIsSettings(true);
  };
  const closeModalHandler = () => {
    setIsSettings(false);
  };

  return (
    <>
      {isSettings && (
        <Modal>
          <UpdateProjectForm
            updateProjectHandler={props.updateProjectHandler}
            closeModalHandler={closeModalHandler}
            project={{
              id: props.projectId,
              color: props.color,
              title: props.title,
              description: props.description,
            }}
          />
        </Modal>
      )}
      <article
        className={`${styles["project-card"]} ${
          isProjectOpened && styles["project-card--active"]
        }`}
      >
        <div
          className={`${styles["project-card__wrapper"]} ${
            isProjectOpened ? styles["project-card__wrapper--opened"] : ""
          }`}
        >
          <span
            style={{ backgroundColor: props.color }}
            className={styles["project-card__color-marker"]}
          ></span>
          <h2 className={styles["project-card__title"]}>{props.title}</h2>
          <BinButton onClick={handleDeleteProject} />
          <Svg
            onClick={openModalHandler}
            className={styles["project-card__settings"]}
            width="32"
            height="32"
            href="/src/assets/settings.svg"
            svgId="settings"
          />
          <ProgressBar
            className={styles["project-card__progress-bar"]}
            size={80}
            color="#77FE74"
            percentageCompleted={
              props.tasks.length !== 0
                ? (doneTasksCount / props.tasks.length) * 100
                : 0
            }
          />
          <Button
            type="open"
            onClick={openProjectHandler}
            className={styles["project-card__open-btn"]}
          ></Button>
        </div>
        <ProjectDescription
          description={props.description}
          tasks={props.tasks}
          isHidden={!isProjectOpened}
          projectId={props.projectId}
          onTaksIsDoneHandler={onTaksIsDoneHandler}
          onAddTaskHandler={props.onAddTaskHandler}
          handleDeleteTask={handleDeleteTask}
          updateTaskHandler={props.updateTaskHandler}
        />
      </article>
    </>
  );
};

export default ProjectCard;
