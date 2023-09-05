import { useState } from "react";
import { UploadWrapper } from "./styles";

import IconSvg from "./icon.svg";

export default function InputUpload({ cb }: { cb: (file: File, url: string) => void; }) {

  const [inDrop, setInDrop] = useState(false);

  async function handleUpload(file: File, e: any) {
    if(e?.target.files && e?.target.files.length <= 0) return null;

    if(file.size > (1024 * 1024 * 40)) return alert("Documento muito grande! Limite máximo: 40 MB");
    if(file.type !== "image/png" && file.type !== "image/jpg" && file.type !== "image/jpeg" && file.type !== "application/pdf") return alert("Tipo de documento não suportado!");
    
    // Carregar
    cb(file, URL.createObjectURL(file));
  }

  function dropHandler(ev: any) {
    ev.preventDefault();
  
    if (ev.dataTransfer.items) {
      [...ev.dataTransfer.items].forEach((item, i) => {
        if (item.kind === "file") {
          const file = item.getAsFile();
          handleUpload(file, ev);
        }
      });
    }

    setInDrop(false);
  }

  function dragOverHandler(ev: any) {
    ev.preventDefault();
    setInDrop(true);
  }

  return <UploadWrapper className="UploadWrapper">
    <div id="overlay"
      onDrop={dropHandler}
      onDragOver={dragOverHandler}
      onDragLeave={() => setInDrop(false)}
      onClick={() => {
        const inputFile = document.getElementById("inputFile");
        inputFile?.click();
      }}
      />

    <input type="file" id="inputFile" onChange={(e: any) => {
      handleUpload(e.target.files[0], e)
      e.target.value = null;
    }} accept="image/png,image/jpg,image/jpeg"  />

    <div className={`content ${inDrop ? "active" : ""}`}>
      <IconSvg />
      <div className="text">
        { inDrop ? <h4>Solte uma imagem</h4>
        : <>
          <h4>Arraste uma imagem</h4>
          <button>Procurar imagem</button>
        </>}
      </div>
    </div>
  </UploadWrapper>
}