// eslint.config.js
import reactConfig from "eslint-config-react-app";
import fsdImportPlugin from "eslint-plugin-fsd-import";

export default [
  ...reactConfig,
  {
    plugins: {
      "fsd-import": fsdImportPlugin,
    },
    rules: {
      "fsd-import/layer-imports": [
        "error",
        {
          layersOrder: [
            "app",
            "pages",
            "widgets",
            "features",
            "entities",
            "shared",
          ],
          ignoreImportPatterns: ["**/testing"],
        },
      ],
      "import/no-internal-modules": [
        "error",
        {
          forbid: [
            "app/**/*",
            "pages/**/*",
            "widgets/**/*",
            "features/**/*",
            "entities/**/*",
          ],
        },
      ],
    },
  },
];
