import { useState } from "react";
import TaskList from "../../Tasks/TaskList/TaskList";
import Button from "../../UI/Button/Button";
import Modal from "../../UI/Modal/Modal";
import AddTaskForm from "../../Tasks/AddTaskForm/AddTaskForm";
import styles from "./ProjectDescription.module.less";

const ProjectDescription = (props) => {
  const [isAddingTask, setIsAddingTask] = useState(false);

  const openModalHandler = () => {
    setIsAddingTask(true);
  };
  const closeModalHandler = () => {
    setIsAddingTask(false);
  };

  return (
    <>
      {isAddingTask && (
        <Modal>
          <AddTaskForm
            handleTask={props.onAddTaskHandler}
            closeModalHandler={closeModalHandler}
            projectId={props.projectId}
          />
        </Modal>
      )}
      <div
        style={{
          maxHeight: props.isHidden ? 0 : "100em",
          paddingTop: props.isHidden ? 0 : 65,
          paddingBottom: props.isHidden ? 0 : 65,
          overflow: "hidden",
        }}
        className={`${styles["project-description"]}`}
      >
        <div
          style={{
            opacity: props.isHidden ? 0 : 1,
            transition: "opacity 0.2s",
          }}
        >
          <p className={styles["project-description__text"]}>
            {props.description}
          </p>
          <Button
            type="add"
            className={styles["project-description__add-btn"]}
            size={"small"}
            onClick={openModalHandler}
          >
            Создать задачу...
          </Button>
          <TaskList
            onTaksIsDoneHandler={props.onTaksIsDoneHandler}
            tasks={props.tasks}
            handleDeleteTask={props.handleDeleteTask}
            updateTaskHandler={props.updateTaskHandler}
          />
        </div>
      </div>
    </>
  );
};

export default ProjectDescription;
