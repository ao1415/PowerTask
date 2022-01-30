module.exports = {
  pluginOptions: {
    electronBuilder: {
      nodeIntegration: true,
      builderOptions: {
        win: {
          target: [
            {
              target: 'portable',
              arch: [
                'x64'
              ]
            }
          ]
        }
      }
    },
    vuetify: {
			// https://github.com/vuetifyjs/vuetify-loader/tree/next/packages/vuetify-loader
		}
  },
};
