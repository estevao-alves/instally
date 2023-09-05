export const numberPattern = /\d+/g;

export function formatPhoneNumber(phone: string) {
  if(!phone) return '';

  phone = String(String(phone).match(numberPattern)?.join(''));
  phone = phone.replace(/[^\d]/g, '')

  var phoneFormatted;

  phoneFormatted = phone.length >= 1 ? `(${phone.slice(0,2)})` : phone;
  
  if(phone[2] === "9") {
    phoneFormatted = phone.length > 2 ? `(${phone.slice(0,2)}) ${phone.slice(2,7)}` : phoneFormatted;
    phoneFormatted = phone.length > 7 ? `(${phone.slice(0,2)}) ${phone.slice(2,7)}-${phone.slice(7,11)}` : phoneFormatted;
  } else {
    phoneFormatted = phone.length > 2 ? `(${phone.slice(0,2)}) ${phone.slice(2,6)}` : phoneFormatted;
    phoneFormatted = phone.length > 6 ? `(${phone.slice(0,2)}) ${phone.slice(2,6)}-${phone.slice(6,10)}` : phoneFormatted;
  }

  return phoneFormatted;
}

export function formatOnlyNumbers(text: string) {
  if(!text) return '';

  text = String(String(text).match(numberPattern)?.join(''));
  text = text.replace(/[^\d]/g, '')

  return text;
}

export function formatMoney(originalValue: string | number = '', options?: any) {
  if(typeof originalValue !== "number" && typeof originalValue !== "string") return '';

  const { editing } = options || {};
  
  if(typeof originalValue === "string" && !String(originalValue)?.includes("R$") && !editing) originalValue = Number(originalValue).toFixed(2)

  const _value = String(originalValue).split("R$").join("").split(",").join("").split(".").join("").split(" ").join("");
  var total = String(String(_value).match(numberPattern)?.join(''));
  
  const _first = String(total).split('')[0];
  if(_first !== '0' && _first !== '1' && _first !== '2' && _first !== '3' && _first !== '4' && _first !== '5' && _first !== '6' && _first !== '7' && _first !== '8' && _first !== '9') return '';
  
  if(_first === '0' && total.length > 3) total = total.slice(1, 4);

  if(total.length < 1) return ``;
  
  const centavos = total.slice(-2);
  const centenas = total.slice(-5, -2);
  const milhares = total.slice(-8, -5);
  const milhoes = total.slice(-11, -8);
  const bilhoes = total.slice(-14, -11);
  const trilhoes = total.slice(-17, -14);

  total = `${trilhoes && `${trilhoes}.`}${bilhoes && `${bilhoes}.`}${milhoes && `${milhoes}.`}${milhares && `${milhares}.`}${centenas && `${centenas},`}${centavos}`;

  return `R$ ${total}`;
}

export function formatFullName(name: string)
{
  const nameSplited = name?.trim()?.split(" ");
  var userName = "";
  
  nameSplited.forEach((namePart: string, index: number) => {

    const linkNames = [ "da", "de", "di", "do", "du", "das", "dos" ];

    if(index === 0 || index === nameSplited?.length-1) return userName += ` ${namePart}`;

    if(linkNames?.filter(link => link === namePart)[0]) return userName += ` ${namePart?.toLowerCase()}`;
    
    return userName += ` ${namePart?.slice(0,1)}.`;
  })

  return userName;
}

type DocumentTypes = {
  hideLastCharacters?: boolean;
};

export function formatDocument(document: string, options?: DocumentTypes) {
  if(!document) return null;

  document = document?.replace(/[^0-9 ]/g, '');
  
  if(options?.hideLastCharacters) document = `${document.slice(0,-5)}*****`;

  var documentFormatted = document;

  if(document.length > 3) documentFormatted = `${document.slice(0,3)}.${document.slice(3,6)}`
  if(document.length > 6) documentFormatted = `${document.slice(0,3)}.${document.slice(3,6)}.${document.slice(6,9)}`
  if(document.length > 9) documentFormatted = `${document.slice(0,3)}.${document.slice(3,6)}.${document.slice(6,9)}-${document.slice(9,11)}`
  if(document.length >= 12) documentFormatted = `${document.slice(0,2)}.${document.slice(2,5)}.${document.slice(5,8)}/${document.slice(8,12)}${document.length >= 13 ? '-' : ''}${document.slice(12,14)}`

  return documentFormatted;
}
