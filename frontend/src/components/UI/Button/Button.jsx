import styles from "./Button.module.less";

const Button = (props) => {
  const types = {
    submit: styles["button__submit"],
    add: styles[`button__add${props.size === "small" ? "--small" : ""}`],
    open: styles["button__open"],
    close: styles["button__close"],
  };

  return (
    <div className={`${styles["button"]} ${props.className}`}>
      <button
        onClick={props.onClick}
        type={`${props.type !== "submit" ? "button" : "submit"}`}
        className={`${types[props.type]}`}
      >
        {props.children}
      </button>
    </div>
  );
};

export default Button;
