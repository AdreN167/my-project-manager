import { useState } from "react";

const useInput = (validateValueFunc) => {
  const [value, setValue] = useState("");
  const [wasInputTouched, setWasInputTouched] = useState(false);

  const isValueValid = validateValueFunc(value);
  const isInputInvalid = !isValueValid && wasInputTouched;

  const changeInputHandler = (e) => {
    setValue(e.target.value);
  };
  const blurInputHandler = () => setWasInputTouched(true);

  const resetValues = () => {
    setValue("");
    setWasInputTouched(false);
  };

  return {
    value: value,
    hasError: isInputInvalid,
    isValid: isValueValid,
    changeInputHandler,
    blurInputHandler,
    resetValues,
  };
};

export default useInput;
