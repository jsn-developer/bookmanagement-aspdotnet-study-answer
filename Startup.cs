using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace BookManagementApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // フレームワークサービスの追加
            services.AddMvc();

            // セッションの追加
            services.AddSession();

            // 検証エラー時のデフォルトメッセージの変更
            services.AddRazorPages().AddMvcOptions(options => {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor( _ => "The field is invalid.");

            });

            // DBコンテキストの登録
            services.AddDbContext<BookManagementAppContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("BookManagementAppContext"),ServerVersion.AutoDetect(Configuration.GetConnectionString("BookManagementAppContext"))));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*
                開発モードで実行しているかチェック
                開発モードの場合、UseDeveloperExceptionPageで開発用の例外ページを設定
                開発モード出ない場合、/Home/Errorをエラーページとしてハンドリングし、STS(Strict-Transport-Security)使用のためのミドルウェアを追加
            */
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // HTTPをHTTPSにリダイレクトする
            app.UseHttpsRedirection();

            // 静的ファイルの利用を可能にする
            app.UseStaticFiles();

            // ルーティングの機能を利用可能にする
            app.UseRouting();

            // 認証機能を利用可能にする
            app.UseAuthorization();

            // セッションを利用可能にする
            app.UseSession();

            /*
                エンドポイントの設定
                ラムダ式でルートの設定を行っている
            */
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
