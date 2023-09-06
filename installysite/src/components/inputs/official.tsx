import { useEffect } from "react";
import { styled } from "styled-components";

const Wrapper = styled.div<{ hasError?: boolean }>`
  background: #EDEDED;
  position: relative;
  border-radius: 16px;
  margin: ${({ hasError }) => hasError ? "0 0 35px" : "0 0 10px"};
  border: ${({ hasError }) => hasError ? "2px solid #cd464699" : "0"};
  
  span {
    position: absolute;
    font-size: 18px;
    font-weight: 500;
    top: 8px;
    left: 20px;
    z-index: 1;
    opacity: .8;
  }
  
  input {
    width: 100%;
    z-index: 2;
    position: relative;
    background: transparent;
    border: none;

    padding: 32px 20px 10px;
    font-size: 24px;
    font-weight: 600;

    &::placeholder {
      opacity: .25;
      color: black;
    }
  }

  span.error {
    font-size: 14px;
    right: 10px;
    bottom: -22px;
    left: initial;
    top: initial;
    color: #cd4646;
  }

  @media (max-width: 576px) {
    span {
      font-size: 16px;
    }
    
    input {
      font-size: 18px;
    }
  }
`;

type InputTypes = {
  type?: string;
  title: string;
  placeholder: string;
  value?: string;
  onChange: (ev: any) => void;
  error?: string;
}

export default function Input({ type, title, placeholder, value, onChange, error }: InputTypes) {

  return (
    <Wrapper hasError={!!error ? true : undefined}>
      <span>{title}</span>
      <input type={type || "text"} placeholder={placeholder} value={value} onChange={onChange} />
      {error ? <span className="error">{error}</span> : <></>}
    </Wrapper>
  )
}