module.exports = (ctx) => {
    // Get the file path if available
    console.log("HHEEEEFSDFQSDFDSQF")
    console.log(ctx);
    console.log("HHEEEEFSDFQSDFDSQF")
    const file = ctx.file || {};
    const dirname = file.dirname || "";
  
    // Determine the correct Tailwind config path based on the directory
    const appPath = dirname.includes('ui') ? 'projects/ui/tailwind.config.js' :
                    dirname.includes('web') ? 'projects/web/tailwind.config.js' :
                    'tailwind.config.js'; // Fallback to a default config
  
    return {
      plugins: {
        tailwindcss: { config: appPath },
        autoprefixer: {},
      },
    };
  };