import styles from "./ProgressBar.module.less";

const ProgressBar = (props) => {
  const progressStyleUnder50 = {
    width: props.size,
    height: props.size,
    backgroundColor: props.color,
    backgroundImage: `linear-gradient(${
      90 + (360 * props.percentageCompleted) / 100
    }deg, transparent 50%, white 50%), linear-gradient(90deg, white 50%, transparent 50%)`,
  };
  const progressStyleOver50 = {
    width: props.size,
    height: props.size,
    backgroundColor: props.color,
    backgroundImage: `
    linear-gradient(${
      270 + (360 * props.percentageCompleted) / 100
    }deg, transparent 50%, ${props.color} 50%),
        linear-gradient(90deg, white 50%, transparent 50%)`,
  };

  const offset = 20;
  const innerWidth = props.size - offset;

  return (
    <div className={`${styles["progress-bar"]} ${props.className}`}>
      <div
        className={styles["progress-bar__percentage"]}
        style={
          props.percentageCompleted > 50
            ? progressStyleOver50
            : progressStyleUnder50
        }
      ></div>
      <p
        className={styles["progress-bar__text"]}
        style={{
          width: innerWidth,
          height: innerWidth,
          top: offset / 2,
          left: offset / 2,
        }}
      >
        {Math.floor(props.percentageCompleted)}%
      </p>
      <div
        className={styles["progress-bar__background"]}
        style={{ width: props.size, height: props.size }}
      ></div>
    </div>
  );
};

export default ProgressBar;
