import styles from "./TextArea.module.less";

const TextArea = (props) => {
  return (
    <label className={`${styles.textarea} ${props.className}`}>
      <span className={styles.textarea__label}>{props.children}</span>
      <textarea
        className={styles.textarea__textarea}
        name={props.name}
        placeholder={props.placeholder}
        required={props.isRequired}
        defaultValue={props.value}
        onChange={props.onChange}
      ></textarea>
    </label>
  );
};

export default TextArea;
