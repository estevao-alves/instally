import React from "react";
import { styled } from "styled-components";

const WindowModal = styled.div<{ active?: boolean }>`
  position: fixed;
  z-index: 1001;
  bottom: ${({ active }) => active ? '0' : '-100vh'};
  left: 0;

  transition: ${({ active }) => active ? '400ms ease-in-out, 400ms background-color 500ms ease-in-out' : '200ms ease-in-out, 0ms background-color 0s ease-in-out'};
  
  display: flex;
  align-items: center;
  justify-content: center;
  
  width: 100vw;
  height: 100%;
  background-color: ${({ active }) => active ? 'rgba(118,118,118,.3)' : 'transparent'};
  backdrop-filter: ${({ active }) => active ? 'blur(10px)' : 'none'};

  > div > * {
    transition: ${({ active }) => active ? '200ms ease-in-out .2s' : '100ms ease-in-out 0ms'};
    opacity: ${({ active }) => active ? '1' : '0'};
  }
`;

const WindowBox = styled.div<{ maxWidth?: string, maxHeight?: string }>`
  max-height: ${({ maxHeight }) => maxHeight || "80vh"};
  overflow: hidden;
  
  width: 100%;
  max-width: ${({ maxWidth }) => maxWidth || "600px"};

  margin: 0 40px;
  background: #fff;
  border-radius: 8px;

  .CategoryTitle {
    &::after {
      display: none;
    }
  }

  .UploadWrapper {
    padding: 0;
    margin: 30px 0 0;
  }

  @media (max-width: 576px) {
    max-height: 100%;
    height: 100%;
    width: 100vw;
    margin: 0;
    border-radius: 0;
  }
`;

const Header = styled.div<{ hasTitle?: boolean }>`
  position: relative;
  height: ${({ hasTitle }) => hasTitle ? "60px" : "0"};
  z-index: 2;
  
  .title {
    height: 100%;
    width: 100%;
    
    display: flex;
    align-items: center;
    
    font-weight: 600;
    color: rgba(120, 120, 120, 1);
    
    padding: 10px 30px;
    border-bottom: 1px solid rgba(0,0,0,.1);
  }
  
  .close {
    --size: 40px;
    border-radius: 100%;
    width: var(--size);
    height: var(--size);
    position: absolute;
    right: 10px;
    top: 10px;
    
    cursor: pointer;
    transition: 200ms ease-in-out;
    
    &:hover {
      background: rgba(0,0,0,.1);
    }

    &::before, &::after {
      content: "";
      width: 50%;
      height: 4px;
      background: rgba(135, 135, 135, 1);
      border-radius: 10px;

      position: absolute;
      top: 0;
      bottom: 0;
      left: 0;
      right: 0;
      margin: auto;
      transform: rotate(45deg);
    }

    &::after {
      transform: rotate(-45deg);
    }
  }
`;

const Content = styled.div<{ maxHeight?: string }>`
  padding: 40px;
  height: ${({ maxHeight }) => maxHeight || "60vh"};
  overflow: auto;
  position: relative;

  @media (max-width: 576px) {
    height: 100%;
  }

  @media (min-width: 577px) {
    &::-webkit-scrollbar {
      width: 10px;
    }
    &::-webkit-scrollbar-thumb {
      border-width: 4px;
    }
  }

  @media (max-width: 480px) {
    padding: 20px;
  }
`;

type WindowTypes = {
  title?: string;
  maxWidth?: string;
  maxHeight?: string;
  status: boolean;
  close: Function;
  children: React.ReactNode;
}

export default function Windows({ title, maxWidth, maxHeight, status, close, children }: WindowTypes) {
  return <WindowModal active={status ? true : undefined}>
    <WindowBox maxWidth={maxWidth} maxHeight={maxHeight}>
      <Header hasTitle={title?.length ? true : undefined}>
        {title ? <div className="title">{title}</div> : <></>} 
        <div className="close" onClick={() => close()} />
      </Header>
      <Content maxHeight={maxHeight}>{children}</Content>
    </WindowBox>
  </WindowModal>
}