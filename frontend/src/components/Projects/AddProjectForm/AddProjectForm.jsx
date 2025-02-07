import Button from "../../UI/Button/Button";
import Field from "../../UI/Field/Field";
import TextArea from "../../UI/TextArea/TextArea";
import ColorPicker from "../../UI/ColorPicker/ColorPicker";
import styles from "./AddProjectForm.module.less";
import { useState, useRef } from "react";
import RGBAToHexA from "../lib";
import axios from "axios";

const AddProjectForm = (props) => {
  // useRef - хук, возвращающий узел DOM
  // refs лучше использовать, когда нам не нужно постоянно ре-рендерить компонент (то есть не нужно менять визуал)
  // но передавать эти ссылки в дочерние компоненты нельзя
  // const inputTitleRef = useRef();
  // const inputDescriptionRef = useRef();
  // <input ref={inputTitleRef} />
  // inputTitleRef.current.value

  const [inputTitle, setInputTitle] = useState("");
  const [inputDescription, setInputDescription] = useState("");
  const [inputColor, setInputColor] = useState("rgba(241, 112, 19, 1)");
  const [error, setError] = useState("");

  const addProjectHandler = async (e) => {
    e.preventDefault();

    try {
      const dtoProject = (
        await axios.post(`api/v1/Project`, {
          name: inputTitle,
          description: inputDescription,
          color: inputColor,
          userLogin: localStorage.getItem("login"),
        })
      ).data.data;

      props.closeModalHandler();
      props.handleProject({
        color: RGBAToHexA(inputColor),
        title: inputTitle,
        description: inputDescription,
        tasks: [],
      });
    } catch (ex) {
      const { response } = ex;
      if (response.data.errorCode === 2) {
        setError("Проект с таким названием уже существует");
      }
    }
  };

  const onClickHandler = () => {
    props.closeModalHandler();
  };

  const changeInputTitleHandler = (e) => {
    setInputTitle(e.target.value);
  };

  const changeInputDescriptionHandler = (e) => {
    setInputDescription(e.target.value);
  };

  const changeColorHandler = (color) => {
    setInputColor(`rgba(${color.r}, ${color.g}, ${color.b}, ${color.a})`);
  };

  return (
    <form
      className={`${styles.form} ${props.className}`}
      onSubmit={addProjectHandler}
    >
      <div className={styles.form__wrapper}>
        <Button
          className={styles.form__close}
          onClick={onClickHandler}
          type="close"
        ></Button>
        <div className={styles.form__container}>
          <p className={styles.form__title}>Создание проекта...</p>
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
          {error !== "" && <p className={styles.form__fail}>{error}</p>}
          <Button className={styles.form__submit} type="submit">
            Создать
          </Button>
        </div>
      </div>
    </form>
  );
};

export default AddProjectForm;
