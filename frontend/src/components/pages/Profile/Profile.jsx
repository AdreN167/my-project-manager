import styles from "./Profile.module.less";
import Button from "../../UI/Button/Button";
import PageHeader from "../../UI/PageHeader/PageHeader";
import ProjectList from "../../Projects/ProjectList/ProjectList";
import AddProjectForm from "../../Projects/AddProjectForm/AddProjectForm";
import Modal from "../../UI/Modal/Modal";
import { useState, useEffect } from "react";
import axios from "axios";

const Profile = (props) => {
  const loadData = async () => {
    const modelProjects = (
      await axios.get(
        `api/v1/Project/userProjects/${localStorage.getItem("login")}`
      )
    ).data.data;

    const dtoProjects = modelProjects.map((proj) => {
      return {
        id: proj.id,
        color: proj.color,
        title: proj.name,
        description: proj.description,
        tasks: [],
      };
    });

    for (let i = 0; i < dtoProjects.length; i++) {
      try {
        const tasks = (
          await axios.get(
            `api/v1/ProjectTask/projectTasks/${dtoProjects[i].id}`
          )
        ).data.data;
        dtoProjects[i].tasks = tasks;
      } catch {
        console.log("tasks not found");
      }
    }

    setProjects(dtoProjects);
  };

  const [projects, setProjects] = useState([]);

  useEffect(() => {
    loadData();
  }, []);

  const [isAddingProject, setIsAddingProject] = useState(false);

  const openModalHandler = () => {
    setIsAddingProject(true);
  };

  const closeModalHandler = () => {
    setIsAddingProject(false);
  };

  const handleProject = (project) => {
    loadData();
  };

  const handleDeleteProject = (title) => {
    const newProjs = projects.filter((proj) => proj.title !== title);
    setProjects(newProjs);
  };

  const handleAddTask = () => {
    loadData();
  };

  const handleDeleteTask = () => {
    loadData();
  };

  const updateProjectHandler = () => {
    loadData();
  };

  const updateTaskHandler = () => {
    loadData();
  };

  const logOutHandler = () => {
    localStorage.removeItem("token");
    window.location.reload();
  };

  if (isAddingProject) document.body.style.overflow = "hidden";
  else document.body.style.overflow = "auto";

  return (
    <div className={styles.profile}>
      {isAddingProject && (
        <Modal>
          <AddProjectForm
            handleProject={handleProject}
            closeModalHandler={closeModalHandler}
          />
        </Modal>
      )}

      <PageHeader logOutHandler={logOutHandler} />
      <div className={styles.profile__container}>
        <Button
          type="add"
          className={styles["profile__add-btn"]}
          onClick={openModalHandler}
        >
          Создать проект...
        </Button>
        <ProjectList
          projects={projects}
          handleDeleteProject={handleDeleteProject}
          handleAddTask={handleAddTask}
          handleDeleteTask={handleDeleteTask}
          updateProjectHandler={updateProjectHandler}
          updateTaskHandler={updateTaskHandler}
        />
      </div>
    </div>
  );
};

export default Profile;
