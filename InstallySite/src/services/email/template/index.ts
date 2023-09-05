import configs from "../../../../configs";
import head from "./head";

type mailTemplateTypes = {
  message: string;
  button?: {
    text: string;
    url: string;
  }
}

export const mailTemplate = ({ message, button }: mailTemplateTypes) => `
  <!DOCTYPE html>
  <html>
  ${head}
  
  <body style='color:#444; font-family:Arial, "Helvetica Neue", Helvetica, sans-serif; font-size:14px; line-height:1.5; margin:0'>
    <table class="wrapper-table" cellpadding="5" cellspacing="0" width="100%" border="0" style="border-collapse:collapse; font-size:14px; line-height:1.5; background-color:#f1f1ef; background-repeat:no-repeat; background-position:left top" bgcolor="#f1f1ef">
      <tr style="border-color:transparent">
      <td align="center" style="border-collapse:collapse; border-color:transparent;padding:40px 0;">
      <table cellpadding="0" cellspacing="0" width="500px" id="bodyTable" border="0" bgcolor="#ffffff" style="border-collapse:collapse; font-size:14px; line-height:1.5">
      <tr style="border-color:transparent">
      <td border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse; border-color:transparent">
        <table cellpadding="0" cellspacing="0" style="border-collapse:collapse; font-size:14px; line-height:1.5; width:100%" border="0" width="100%">
          <tr style="border-color:transparent">
            <th width="500" style="border-color:transparent; font-weight:400; text-align:left; vertical-align:top" cellpadding="0" cellspacing="0" class="tc" align="left" valign="top">
              <table border="0" width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse; font-size:14px; line-height:1.5; background-color:#fff" bgcolor="#ffffff">
                <tr style="border-color:transparent">
                  <td cellpadding="0" cellspacing="0" style="border-collapse:collapse; border-color:transparent">
                    <table width="100%" cellpadding="0" cellspacing="0" id="wout_block_8_element_0" style="border-collapse:collapse; font-size:14px; line-height:1.5">
                      <tr class="content-row" style='border-color:transparent; color:#444; font-family:Arial, "Helvetica Neue", Helvetica, sans-serif'>
                        <td class="content-cell" width="470" style="border-collapse:collapse; border-color:transparent; vertical-align:top; padding-left:15px; padding-right:15px; padding-top:60px; padding-bottom:15px" valign="top">
                          <div id="wout_block_8_element_0" style="font-size:14px; line-height:1.5; width:100%; height:235; display:block; text-align:center" width="100%" height="235" align="center">
                            <center>
                              <img border="0" width="240" height="auto" class=" sp-img " align="center" alt="CriarPagina.com" src="${configs.domain + "/logo.png"}"
                                style="max-width: 240px; height:auto; line-height:100%; outline:0; text-decoration:none; border:0; display:block; -ms-interpolation-mode:bicubic">
                            </center>
                          </div>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </th>
          </tr>
        </table>

        <table cellpadding="0" cellspacing="0" style="border-collapse:collapse; font-size:14px; line-height:1.5; width:100%" border="0" width="100%">
          <tr style="border-color:transparent">
            <th width="500" style="border-color:transparent; font-weight:400; text-align:left; vertical-align:top" cellpadding="0" cellspacing="0" class="tc" align="left" valign="top">
              <table border="0" width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse; font-size:14px; line-height:1.5; background-color:#fff" bgcolor="#ffffff">
                <tr style="border-color:transparent">
                  <td cellpadding="0" cellspacing="0" style="border-collapse:collapse; border-color:transparent">
                    <table width="100%" cellpadding="0" cellspacing="0" id="wout_block_out_block_5" style='border-collapse:collapse; font-size:14px; line-height:1.5; text-color:black; font-family:"Helvetica Neue", Helvetica, "Neue Haas Grotesk Text Pro", Arial, sans-serif; font-family-short:helvetica; font-weight:normal; margin:0'>
                      <tr class="content-row" style='border-color:transparent; color:#444; font-family:Arial, "Helvetica Neue", Helvetica, sans-serif'>
                        <td class="content-cell" width="440" style="border-collapse:collapse; border-color:transparent; vertical-align:top; padding-left:30px; padding-right:30px; padding-top:30px; padding-bottom:15px" valign="top">
                          <p style='line-height:inherit; margin:0 0 10px; font-size:inherit; color:inherit; font-family:"Helvetica Neue", Helvetica, "Neue Haas Grotesk Text Pro", Arial, sans-serif; font-weight:normal; padding:0'>${message}</p>
                          <div style="font-size:14px; line-height:1.5; clear:both"></div>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
              
              ${button ? `
                <table border="0" width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse; font-size:14px; line-height:1.5; background-color:#fff" bgcolor="#ffffff">
                  <tr style="border-color:transparent">
                    <td cellpadding="0" cellspacing="0" style="border-collapse:collapse; border-color:transparent">
                      <table width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse; font-size:14px; line-height:1.5">
                        <tr class="content-row" style='border-color:transparent; color:#444; font-family:Arial, "Helvetica Neue", Helvetica, sans-serif'>
                          <td class="content-cell padding-top-0 padding-bottom-0" width="540" style="border-collapse:collapse; border-color:transparent; vertical-align:top; padding-left:30px; padding-right:30px; padding-top:0; padding-bottom:0" valign="top">
                            <table cellpadding="0" border="0" cellspacing="0" align="left" class="sp-button flat auto-width" style="border-collapse:collapse; font-size:14px; line-height:1.5; border-width:0; border-style:solid; border-color: transparent; width:auto !important; border-radius: 100px; box-shadow:none; background: #2aa467; color:#FFF;" width="auto !important">
                              <tbody>
                                <tr style="border-color:transparent">
                                  <td class="sp-button-text" style="border-collapse:collapse; border-color:transparent; padding:0; border-width:0; border-style:none; border:0; align:left; border-radius:6px; width:auto; height:45px; vertical-align:middle; text-align:center" width="auto" height="45" valign="middle" align="center">
                                    <table cellpadding="0" border="0" cellspacing="0" width="100%" style="border-collapse:collapse; font-size:14px; line-height:1.5; border:0">
                                      <tr style="border-color:transparent">
                                        <td align="center" style="border-collapse:collapse; border-color:transparent; padding:0; border:0; line-height:1">
                                          <a style='text-decoration:none; color:#FFF; display:block; padding:14.5px 21.75px; font-family:Arial, "Helvetica Neue", Helvetica, sans-serif; font-family-short:arial; font-size:16px; font-weight:bold' href="${button.url}">
                                            ${button.text}
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                              </tbody>
                            </table>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>`
              : ''}
              
              <table border="0" width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse; font-size:14px; line-height:1.5; background-color:#fff" bgcolor="#ffffff">
                <tr style="border-color:transparent">
                  <td cellpadding="0" cellspacing="0" style="border-collapse:collapse; border-color:transparent">
                    <table width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse; font-size:14px; line-height:1.5">
                      <tr class="content-row" style='border-color:transparent; color:#444; font-family:Arial, "Helvetica Neue", Helvetica, sans-serif'>
                        <td class="content-cell padding-top-0 padding-bottom-0" width="540" style="border-collapse:collapse; border-color:transparent; vertical-align:top; padding-left:30px; padding-right:30px; padding-top:0; padding-bottom:0" valign="top">
                          <div style="font-size:14px; line-height:1.5; clear:both; padding-top: 30px; padding-bottom: 30px;">
                            <p style='line-height:inherit; margin:0 0 10px; font-size:inherit; color:inherit; font-family:"Helvetica Neue", Helvetica, "Neue Haas Grotesk Text Pro", Arial, sans-serif; font-weight:normal; padding:0'>
                              Atenciosamente,
                              <br/>
                              <span style="font-weight:bold;color:#2aa467;">CriarPagina.com</span>
                            </p>
                          </div>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </table>

      </td>
      </tr>
      </table>
      </td>
      </tr>
    </table>
  </body>
`;