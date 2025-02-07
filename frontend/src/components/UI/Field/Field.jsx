import styles from "./Field.module.less";

const Field = (props) => {
  return (
    <label className={`${styles.field} ${props.className}`}>
      <span className={styles.field__label}>{props.children}</span>

      <input
        className={styles.field__input}
        type="text"
        placeholder={props.placeholder}
        name={props.name}
        required={props.isRequired}
        defaultValue={props.value}
        onChange={props.onChange}
      />
    </label>
  );
};

export default Field;
