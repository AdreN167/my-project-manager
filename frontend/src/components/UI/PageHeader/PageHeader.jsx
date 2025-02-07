import styles from "./PageHeader.module.less";

const PageHeader = (props) => {
  return (
    <section className={styles["page-header"]}>
      <div className={styles["page-header__container"]}>
        <picture className={styles["page-header__logo"]}>
          <img
            className={styles["page-header__logo-img"]}
            src="../../../../public/logo.png"
            alt="logo"
          />
        </picture>
        <button
          type="button"
          onClick={props.logOutHandler}
          className={styles["page-header__out"]}
        >
          Выйти
        </button>
      </div>
    </section>
  );
};

export default PageHeader;
