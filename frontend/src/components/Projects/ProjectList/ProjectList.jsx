import ProjectCard from "../ProjectCard/ProjectCard";
import styles from "./ProjectList.module.less";

const ProjectList = (props) => {
  const onTaksIsDoneHandler = (id) => {
    console.log(id);
  };
  return (
    <ul className={styles.listing}>
      {props.projects.map((project) => (
        <li key={project.title} className={styles.listing__item}>
          <ProjectCard
            projectId={project.id}
            onTaksIsDoneHandler={onTaksIsDoneHandler}
            tasks={project.tasks}
            description={project.description}
            color={project.color}
            title={project.title}
            handleDeleteProject={props.handleDeleteProject}
            onAddTaskHandler={props.handleAddTask}
            handleDeleteTask={props.handleDeleteTask}
            updateProjectHandler={props.updateProjectHandler}
            updateTaskHandler={props.updateTaskHandler}
          />
        </li>
      ))}
    </ul>
  );
};

export default ProjectList;
