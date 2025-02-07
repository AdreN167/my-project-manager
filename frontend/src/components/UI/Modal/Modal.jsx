import React, { Fragment } from "react";
import ReactDOM from "react-dom";
import styles from "./Modal.module.less";

const Backdrop = (props) => {
  return <div className={styles.modal__backdrop}></div>;
};

const ModalContent = (props) => {
  return <div className={styles.modal__content}>{props.children}</div>;
};

// портал для семантической разметки
const Modal = (props) => {
  return (
    <Fragment>
      {ReactDOM.createPortal(<Backdrop />, document.getElementById("backdrop"))}
      {ReactDOM.createPortal(
        <ModalContent>{props.children}</ModalContent>,
        document.getElementById("modal")
      )}
    </Fragment>
  );
};

export default Modal;
