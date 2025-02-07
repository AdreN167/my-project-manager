import { useState } from "react";
import Button from "../../UI/Button/Button";
import TextArea from "../../UI/TextArea/TextArea";
import styles from "./UpdateProjectForm.module.less";
import Field from "../../UI/Field/Field";
import ColorPicker from "../../UI/ColorPicker/ColorPicker";
import axios from "axios";

const getColorObj = (color) => {
  const arr = color.slice(5, -1).split(",");
  const col = {
    r: +arr[0],
    g: +arr[1],
    b: +arr[2],
    a: +arr[3],
  };
  return col;
};

const ProjectSettingsForm = (props) => {
  const [inputTitle, setInputTitle] = useState(props.project.title);
  const [inputDescription, setInputDescription] = useState(
    props.project.description
  );
  const [inputColor, setInputColor] = useState(
    getColorObj(props.project.color)
  );

  const updateProjectHandler = async (e) => {
    e.preventDefault();
    const data = {
      id: props.project.id,
      name: inputTitle,
      color: `rgba(${inputColor.r}, ${inputColor.g}, ${inputColor.b}, ${inputColor.a})`,
      description: inputDescription,
    };
    const dtoProject = (await axios.put(`api/v1/Project`, data)).data.data;

    props.closeModalHandler();
    props.updateProjectHandler();
  };

  const onClickHandler = () => {
    props.closeModalHandler();
  };

  const changeInputDescriptionHandler = (e) => {
    setInputDescription(e.target.value);
  };

  const changeInputTitleHandler = (e) => {
    setInputTitle(e.target.value);
  };

  const changeColorHandler = (color) => {
    setInputColor(color);
  };

  return (
    <form
      className={`${styles.form} ${props.className}`}
      onSubmit={updateProjectHandler}
    >
      <div className={styles.form__wrapper}>
        <Button
          className={styles.form__close}
          onClick={onClickHandler}
          type="close"
        ></Button>
        <div className={styles.form__container}>
          <p className={styles.form__title}>Изменения проекта...</p>
          <div className={styles.form__group}>
            <Field
              className={styles.form__input}
              isRequired={true}
              placeholder="Название"
              name="title"
              value={inputTitle}
              onChange={changeInputTitleHandler}
            >
              Название проекта
            </Field>
            <ColorPicker value={inputColor} onChange={changeColorHandler} />
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

export default ProjectSettingsForm;
