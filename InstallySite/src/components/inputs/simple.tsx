import { styled } from "styled-components";

const Wrapper = styled.div<{ hasError?: boolean }>`
  position: relative;
  border: ${({ hasError }) => hasError ? "2px solid #cd464699" : "0"};
  padding: ${({ hasError }) => hasError ? "15px 15px 10px" : "0"};
  margin: ${({ hasError }) => hasError ? "20px -15px" : "10px 0"};
  border-radius: 10px;
  
  input {
    width: 100%;
    z-index: 2;
    position: relative;
    background: transparent;
    border: none;

    background: rgb(229 229 229);
    border-radius: 10px;
    padding: 15px 25px 15px;
    font-size: 16px;
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
  placeholder: string;
  value?: string;
  onChange: (ev: any) => void;
  error?: string;
}

export default function InputSimple({ type, placeholder, value, onChange, error }: InputTypes) {

  return (
    <Wrapper hasError={!!error ? true : undefined}>
      <input type={type || "text"} placeholder={placeholder} value={value} onChange={onChange} />
      {error ? <span className="error">{error}</span> : <></>}
    </Wrapper>
  )
}