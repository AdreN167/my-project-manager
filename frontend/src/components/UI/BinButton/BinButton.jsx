import styles from "./BinButton.module.less";

const BinButton = (props) => {
  return (
    <button
      type="button"
      className={styles["delete-btn"]}
      onClick={props.onClick}
    >
      <svg width="29" height="32">
        <use xlinkHref="/public/symbols.svg#delete"></use>
      </svg>
    </button>
  );
};

export default BinButton;
