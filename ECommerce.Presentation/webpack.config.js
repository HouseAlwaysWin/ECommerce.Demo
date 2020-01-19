/// <binding />
const path = require("path");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const CompressionPlugin = require("compression-webpack-plugin");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const VueLoaderPlugin = require("vue-loader/lib/plugin");

module.exports = (env = {}, argv = {}) => {
  const isProd = process.env.NODE_ENV && process.env.NODE_ENV === "production";
  const devMode = isProd ? "production" : "development";

  const config = {
    mode: devMode ? "development" : "production",

    optimization: {
      minimize: true
    },
    entry: {
      main: "./ClientApp/src/main.js"
    },
    output: {
      filename: isProd ? "bundle-[chunkHash].js" : "[name].js",
      path: path.resolve(__dirname, "./wwwroot/dist"),
      publicPath: "/dist/"
    },
    resolve: { alias: { vue: "vue/dist/vue.esm.js" } },
    plugins: [
      new MiniCssExtractPlugin({
        filename: isProd ? "style-[contenthash].css" : "style.css"
      }),
      new CompressionPlugin({
        filename: "[path].gz[query]",
        algorithm: "gzip",
        test: /\.js$|\.css$|\.html$|\.eot?.+$|\.ttf?.+$|\.woff?.+$|\.svg?.+$/,
        threshold: 10240,
        minRatio: 0.8
      }),
      new HtmlWebpackPlugin({
        filename: path.resolve(__dirname, "Views/Shared/_Layout.cshtml"),
        template: path.resolve(
          __dirname,
          "Views/Shared/_LayoutTemplate.cshtml"
        ),
        inject: false
        //templateParameters: {
        //	baseHref: BaseConfig.baseUriPath,
        //	appName: AppConfig.App.Title
        //}
      }),
      new VueLoaderPlugin()
    ],
    module: {
      rules: [
        {
          test: /\.vue$/,
          loader: "vue-loader"
        },
        {
          test: /\.js$/,
          exclude: /node_modules/,
          use: {
            loader: "babel-loader"
          }
        },
        {
          test: /\.(sa|sc|c)ss$/,
          use: [
            "vue-style-loader",
            "style-loader",
            MiniCssExtractPlugin.loader,
            "css-loader",
            "sass-loader"
          ]
        },
        {
          test: /\.(png|jpg|gif|woff|woff2|eot|ttf|svg)$/,
          loader: "file-loader",
          options: {
            name: "[name].[hash].[ext]",
            outputPath: "assets/"
          }
        }
      ]
    }
  };
  return config;
};
