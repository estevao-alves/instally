import { useEffect, useState } from 'react';

import { InputSimplesContainer, InputContent, ErrorField } from "./styles";

import ViewIcon from '@/assets/icons/view.svg';
import NotViewIcon from '@/assets/icons/notview.svg';

type InputTypes = {
  style?: any;
  value?: string;
  type?: string;
  defaultValue?: string;
  placeholder: string;
  error?: string;
  refs?: any;
  required?: boolean;
  disabled?: boolean;
  onChange: any;
  className?: string;
  icon?: any;
  minLength?: number;
  maxLength?: number;
}

const Input = ({ style, value, type: initialType = 'text', defaultValue, placeholder, error, refs, required, disabled, onChange, className, icon, minLength, maxLength }: InputTypes) => {
  const [viewPassword, setViewPassword] = useState(false);
  const [type, setType] = useState(initialType);

  useEffect(() => {
    if(className === "password") setType(viewPassword ? 'text' : 'password')
  }, [viewPassword])

  return (
    <InputSimplesContainer style={style} className={`${className}${error ? ' error' : ''}`}>
      {icon || ''}
      <InputContent className={className} error={error ? true : undefined} icon={!icon ? 'noIcon' : ''}>
        <input type={type} minLength={minLength} autoComplete="off" maxLength={maxLength} id={placeholder.split(" ").join("").toLowerCase()} ref={refs} defaultValue={defaultValue} value={value} className="_field" placeholder={placeholder} disabled={disabled} onChange={onChange} />
        { placeholder && <label htmlFor={placeholder.split(" ").join("").toLowerCase()} className="_label">{placeholder}</label> }
        { className === "password" && (viewPassword ? <ViewIcon className="view-icon" onClick={() => setViewPassword(false)} /> : <NotViewIcon className="view-icon" onClick={() => setViewPassword(true)} />) }
      </InputContent>
      <ErrorField state={error ? true : undefined}>{error}</ErrorField>
    </InputSimplesContainer>
  )
}

export default Input;