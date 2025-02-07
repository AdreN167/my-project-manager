import TaskCard from "../TaskCard/TaskCard";
import styles from "./TaskList.module.less";

const TaskList = (props) => {
  const tasks = props.tasks;

  return (
    <ul className={styles["task-list"]}>
      {tasks.map((task, index) => (
        <li className={styles["task-list__item"]} key={index}>
          <TaskCard
            onTaksIsDoneHandler={props.onTaksIsDoneHandler}
            id={task.id}
            description={task.description}
            isDone={task.isDone}
            deadline={task.deadline}
            handleDeleteTask={props.handleDeleteTask}
            updateTaskHandler={props.updateTaskHandler}
          />
        </li>
      ))}
    </ul>
  );
};

export default TaskList;
