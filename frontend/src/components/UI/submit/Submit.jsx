import styles from "./Submit.module.less";

const Submit = ({ onClick, children, className, disabled }) => {
  let clasess = styles.submit;
  if (disabled !== undefined) {
    clasess = disabled
      ? styles.submit
      : styles.submit + " " + styles["submit--disabled"];
  }
  return (
    <button
      disabled={disabled !== undefined ? !disabled : false}
      className={clasess + " " + className}
      type="submit"
      onClick={onClick}
    >
      {children}
    </button>
  );
};

export default Submit;
