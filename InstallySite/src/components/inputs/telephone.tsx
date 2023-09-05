import PhoneInput from 'react-phone-input-2';

import styled from "styled-components";

import 'react-phone-input-2/lib/style.css'  

const InputSimplesContainer = styled.div<{ error: boolean }>`
  display: flex;
  flex-direction: column;
  width: 100%;
  position: relative;
  margin: 10px 0;
  
  .react-tel-input {
    position: relative;
    margin: 0 0 10px;
    border-radius: 10px;

    color: #333;
    background: rgb(229 229 229);

    border: 2px solid ${props => props.error ? 'rgb(219, 127, 127)' : 'transparent' };
    transition: .4s ease-in-out;

    > * {
      transition: .4s ease-in-out;
    }

    input {
      cursor: pointer;
      width: 100%;
      max-width: 100%;
      border: 0;
      color: ${props => props.error ? 'rgb(219, 127, 127)' : "#333"};
      background: transparent;
      font-size: 18px;
      font-weight: 500;
      z-index: 2;
      position: relative;
      padding: 15px 20px 15px 56px;
      outline: none!important;
      height: inherit!important;
      max-height: 56px;
      
      &::placeholder {
        color: transparent;
        -webkit-touch-callout: none; /* iOS Safari */
        -webkit-user-select: none; /* Safari */
        -khtml-user-select: none; /* Konqueror HTML */
        -moz-user-select: none; /* Old versions of Firefox */
        -ms-user-select: none; /* Internet Explorer/Edge */
        user-select: none;
      }

      &:placeholder-shown ~ .special-label {
        cursor: text;
        top: 17px;
        font-size: 18px;
        font-weight: 600;
        opacity: .7;
        -webkit-touch-callout: none; /* iOS Safari */
        -webkit-user-select: none; /* Safari */
        -khtml-user-select: none; /* Konqueror HTML */
        -moz-user-select: none; /* Old versions of Firefox */
        -ms-user-select: none; /* Internet Explorer/Edge */
        user-select: none;
        transition: .4s ease-in-out;
      }
      
      &:focus ~ .special-label {
        position: absolute;
        transition: 0.2s;
        top: 8px;
        font-size: 16px;
        font-weight: 600;
      }

      :required, .simple__field:invalid {
        box-shadow: none;
      }

      &:disabled {
        opacity: 0.5;
        cursor: not-allowed;
      }

      &:-webkit-autofill, &:-webkit-autofill:hover, &:-webkit-autofill:focus, &:-webkit-autofill:active {
        -webkit-box-shadow: none!important;
        background: transparent!important;
        outline: none!important;
        -webkit-text-fill-color: rgba(0,0,0,.8)!important;
        -webkit-transition: background-color 5000s ease-in-out 0s;
        transition: background-color 5000s ease-in-out 0s;
      }
    }

    /* .special-label {
      position: absolute;
      display: block;
      -webkit-transition: 0.2s;
      transition: 0.2s;
      font-size: 16px;
      top: 8px;
      left: 60px;
      font-weight: 600;
      color: rgba(0,0,0,.7);
      background: transparent;
      padding: 0;
      z-index: 3;
    } */

  }

  .flag-dropdown {
    z-index: 100!important;
    background: transparent!important;
    border: none!important;
    
    &.open {
      width: 100%;
    }
    
    .selected-flag {
      margin: 4px;
      padding: 0 14px;
      width: 47px;
      height: 47px;
      border-radius: 8px!important;
      
      &:hover {
        background: #d3d3d3;
      }
    }

    .country-list  {
      width: 100%;
      border-radius: 10px;
      text-align: left;
    }
  }
`;

type ErrorTypes = {
  state?: boolean;
}

export const ErrorField = styled.div<ErrorTypes>`
  height: ${props => props.state ? 'auto' : '0'};
  padding: ${props => props.state ? '0 10px 10px' : '0'};
  margin: ${props => props.state ? '-6px 0 0' : '0'};
  border-radius: 10px;
  font-weight: 700;
  -webkit-transition: all .4s;
  color: #db7f7f;
  transition: all .4s;
  font-size: 14px;
  z-index: 2;
  
  display: flex;
  justify-content: flex-end;
`;

export type CountryPhoneTypes = {
  countryCode: string;
  dialCode: string;
  format: string;
  name: string;
}

type InputTypes = {
  value?: string;
  defaultValue?: string;
  error?: string;
  refs?: any;
  onChange?: ((phone: string, country: CountryPhoneTypes) => void) | any;
  className?: string;
  disabled?: boolean;
}

const InputPhone = ({ value, error, onChange, className, disabled = false }: InputTypes) => {

  function handleChange(phone: string, country: CountryPhoneTypes) {
    onChange(phone, country)
  }

  return (
    <InputSimplesContainer error={error ? true : false} className={className}>
      <PhoneInput
        value={value}
        country={'br'}
        disabled={disabled ? true : undefined}
        onChange={(phone, country: CountryPhoneTypes, e, formmatedValue) => !disabled && handleChange(phone, country)}
      />
      <ErrorField state={error ? true : false}>{error}</ErrorField>
    </InputSimplesContainer>
  )
}

export default InputPhone;