import { useState } from "react";
import styles from "./Check.module.less";

const Check = (props) => {
  return (
    <label className={`${styles.check} ${props.className}`}>
      <input
        className={styles.check__input}
        type="checkbox"
        name="task"
        required
        onChange={props.onChangeHandler}
        checked={props.isDone}
      />

      <span className={styles.check__mark}></span>
      <span className={`${styles.check__label} ${styles["check__label--s"]}`}>
        {props.children}
      </span>
    </label>
  );
};

export default Check;
