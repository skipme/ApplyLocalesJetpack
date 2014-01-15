tool provides apply localisation information to xpi/install.rdf for sdk based mozilla firefox plugins;

cmd arguments: [0] xpi file path [1] locale json array file path

json array file must contain json with following structure: [{l: "localeCode", name: "addonName", description: "addon description"}]
in result that configuration would be converted to xml which appends to install rdf file:

<em:localized>
      <Description xmlns="">
        <em:locale>en-US</em:locale>
        <em:name>y-Translate</em:name>
        <em:description>Just move cursor to word which you want to translate. The common solution for inline words translation, maked as replacement of the closed yandex.bar which has a good user experience. Now accepts only ru-en also en-ru translate cases...</em:description>
      </Description>
 </em:localized>

More at "bat xpi" dir!
