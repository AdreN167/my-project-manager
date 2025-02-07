import styles from "./Svg.module.less";

const Svg = (props) => {
  const options = {
    width: props.width,
    height: props.height,
    className: props.className,
    href: props.href,
    svgId: props.svgId,
  };
  return (
    <div className={styles.img + " " + props.className} onClick={props.onClick}>
      <svg
        className={styles.img__icon}
        width={props.width}
        height={props.height}
      >
        <use xlinkHref={options.href + "#" + options.svgId}></use>
      </svg>
    </div>
  );
};

export default Svg;
