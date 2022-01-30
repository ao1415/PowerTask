module.exports = {
  pluginOptions: {
    electronBuilder: {
      nodeIntegration: true,
      builderOptions: {
        win: {
          target: [
            {
              target: "portable",
              arch: ["x64"],
            },
          ],
        },
      },
    },
  },
};
