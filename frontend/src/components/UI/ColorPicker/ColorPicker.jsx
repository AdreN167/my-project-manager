import { useState } from "react";
import styles from "./ColorPicker.module.less";
import { SketchPicker } from "react-color";

const ColorPicker = (props) => {
  const [isOpened, setIsOpened] = useState(false);
  const [color, setColor] = useState(props.value);
  const bgColor = `rgba(${color.r}, ${color.g}, ${color.b}, ${color.a})`;

  const handleClick = (e) => {
    if (e.target.id != "inner") setIsOpened(true);
  };

  const handleClose = (e) => {
    setIsOpened(false);
  };

  const handleChange = (inputColor) => {
    setColor(inputColor.rgb);
    props.onChange(color);
  };

  return (
    <div onClick={handleClick} className={styles.colorpicker}>
      <div
        className={styles.colorpicker__marker}
        style={{ backgroundColor: bgColor }}
      ></div>
      {isOpened && (
        <div className={styles.colorpicker__popover}>
          <div
            id="inner"
            className={styles.colorpicker__cover}
            onClick={handleClose}
          />
          <SketchPicker color={color} onChange={handleChange} />
        </div>
      )}
    </div>
  );
};

export default ColorPicker;
