import { useRef } from "react";
import { styled } from "styled-components";

const Wrapper = styled.div<{ hasError?: boolean, haslimit: number }>`
  position: relative;
  border: ${({ hasError }) => hasError ? "2px solid #cd464699" : "0"};
  margin: 10px 0;
  position: relative;
  overflow: hidden;
  border-radius: 10px;
  
  textarea {
    border-radius: 10px;
    width: 100%;
    z-index: 2;
    position: relative;
    border: none;

    background: rgb(229 229 229);
    padding: ${({ haslimit }) => haslimit === 0 ? "20px 25px" : "35px 25px 25px"};
    font-size: 16px;
    font-weight: 600;

    &::placeholder {
      opacity: .25;
      color: black;
    }
    
    resize: none;

    @media (min-width: 577px) {
      &::-webkit-scrollbar {
        width: 12px!important;
        background: transparent!important;
        padding: 2px!important;
      }
    }
  }

  span.limit {
    position: absolute;
    top: 0;
    right: 10px;
    font-size: 12px;
    font-weight: 600;
    width: 100%;
    background: #e5e5e5;
    z-index: 2;
    text-align: right;
    padding: 10px 10px 5px;
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
  rows?: number;
  placeholder: string;
  value?: string;
  onChange: (ev: any) => void;
  error?: string;
  limit?: number;
}

export default function TextAreaSimple({ rows = 3, placeholder, value, onChange, error, limit }: InputTypes) {

  const elementRef = useRef(null) as any;

  return (
    <Wrapper hasError={!!error ? true : undefined} haslimit={!!limit ? 1 : 0}>
      <textarea ref={elementRef} placeholder={placeholder} value={value} onChange={onChange} rows={rows} />
      { limit ? <span className="limit">{elementRef?.current?.value?.length || 0}/{limit}</span> : <></> }
      {error ? <span className="error">{error}</span> : <></>}
    </Wrapper>
  )
}