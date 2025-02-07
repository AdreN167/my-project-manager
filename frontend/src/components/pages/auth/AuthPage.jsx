import { useState } from "react";
import styles from "./AuthPage.module.less";
import SignUp from "./tabs/signUp/SignUp";
import SignIn from "./tabs/signIn/SignIn";

const AuthPage = ({ onAuth }) => {
  const tabs = [
    {
      id: 1,
      label: "Вход",
    },
    {
      id: 2,
      label: "Регистрация",
    },
  ];
  const leftRadius = { borderTopLeftRadius: "25px" };
  const rightRadius = { borderTopRightRadius: "25px" };

  const [activeTab, setActiveTab] = useState(1);

  return (
    <div className={styles.auth}>
      <div className={styles.auth__container}>
        <ul className={styles.auth__nav}>
          {tabs.map((tab) => {
            return (
              <li
                key={tab.id}
                className={
                  styles["auth__nav-item"] +
                  " " +
                  (activeTab === tab.id && styles["auth__nav-item--active"])
                }
                onClick={() => setActiveTab(tab.id)}
              >
                {tab.label}
              </li>
            );
          })}
        </ul>
        <div
          className={styles.auth__outlet}
          style={
            (activeTab === 1 && rightRadius) || (activeTab === 2 && leftRadius)
          }
        >
          {activeTab === 1 && <SignIn onAuth={onAuth} />}
          {activeTab === 2 && <SignUp onAuth={onAuth} />}
        </div>
      </div>
    </div>
  );
};

export default AuthPage;
