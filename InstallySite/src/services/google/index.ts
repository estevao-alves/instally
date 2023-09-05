import * as gmailApi from "./gmail";
import * as storageApi from "./storage";

const googleServices = {
  gmail: gmailApi,
  storage: storageApi
}

export default googleServices;