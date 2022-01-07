module.exports = {
	pluginOptions: {
		electronBuilder: {
			builderOptions: {
				win: {
					//icon: 'src/assets/icon.png',
					target: [
						{
							target: 'portable',
							arch: ['x64'],
						},
					],
				},
			}
		}
	}
}
