import { ClientFormData } from "@/sections/forms";

export function formValidator(form: ClientFormData) {
  var { tipo, titulo, logo, informacoesRelevantes, imagens, corDestaque, dominio, video, formularioInscricao, avaliacoes, botaoFlutuanteWhatsApp, ctaDestino } = form as ClientFormData;
  
  var error: FormData | any = {};
  
  if(!tipo) error.tipo = "* Escolha uma opção";
  if(!titulo) error.titulo = "* Obrigatório informar";

  if(!logo) error.logo = "* Escolha uma opção";
  if(logo?.inserir && !logo.itens?.length) error.logo = "* Envie uma imagem";
  
  if(!informacoesRelevantes) error.informacoesRelevantes = "* Obrigatório informar";
  
  if(!imagens) error.imagens = "* Escolha uma opção";
  if(imagens?.inserir && !imagens.itens?.length) error.imagens = "* Envie pelo menos uma imagem";

  if(!corDestaque) error.corDestaque = "* Escolha uma cor";

  if(!dominio) error.dominio = "* Escolha uma opção";
  if(dominio && !dominio.url) error.dominio = "* Informe o domínio";

  if(!video) error.video = "* Escolha uma opção";
  if(video?.inserir && !video.url) error.video = "* Informe o endereço do vídeo";

  if(!formularioInscricao) error.formularioInscricao = "* Escolha uma opção";
  
  if(!avaliacoes) error.avaliacoes = "* Escolha uma opção";
  if(avaliacoes?.inserir && !avaliacoes.mensagem) error.avaliacoes = "* Preencha as avaliações";
  
  if(!botaoFlutuanteWhatsApp) error.botaoFlutuanteWhatsApp = "* Escolha uma opção";
  if(botaoFlutuanteWhatsApp?.inserir && !botaoFlutuanteWhatsApp.numero) error.botaoFlutuanteWhatsApp = "* Informe o número";

  if(!ctaDestino) error.ctaDestino = "* Escolha uma opção";
  if(ctaDestino?.destino === "WhatsApp" && !ctaDestino.numero) error.ctaDestino = "* Informe o número";
  if(ctaDestino?.destino === "Link específico" && !ctaDestino.url) error.ctaDestino = "* Informe o link";

  return error;
}