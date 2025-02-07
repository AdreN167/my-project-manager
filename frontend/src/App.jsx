import { useEffect, useState } from "react";
import styles from "./App.module.less";
import Profile from "./components/pages/Profile/Profile";
import AuthPage from "./components/pages/auth/AuthPage";

function App() {
  const [auth, setAuth] = useState(false);

  useEffect(() => {
    if (localStorage.getItem("token") !== null) setAuth(true);
    else setAuth(false);
  }, []);

  const authHandler = () => {
    setAuth(localStorage.getItem("token") !== null);
  };

  return (
    <div className={styles.app}>
      {!auth && <AuthPage onAuth={authHandler} />}
      {auth && <Profile />}
    </div>
  );
}

export default App;
