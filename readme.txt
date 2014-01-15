tool provides apply localisation information to xpi/install.rdf for sdk based mozilla firefox plugins;

cmd arguments: [0] xpi file path [1] locale json array file path

json array file must contain json with following structure: [{l: "localeCode", name: "addonName", description: "addon description"}]
in result that configuration would be converted to xml which appends to install rdf file:

<em:localized>
      <Description xmlns="">
        <em:locale>localeCode</em:locale>
        <em:name>addonName</em:name>
        <em:description>addon description</em:description>
      </Description>
 </em:localized>

More at "bat xpi" dir!
