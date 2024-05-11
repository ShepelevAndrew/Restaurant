using System.Text;

namespace Restaurant.Infrastructure.Auth.VerificationCodeServices;

public static class VerificationCodeEmail
{
    public static string Html(string email, string code)
    {
        var linkToVerification = $"https://localhost:8080/api/v1.0/auth/confirm-email?email={email}&code={code}";
        return
          $$"""
              <!DOCTYPE html>
              <html>
                <head>
                  <title>Email</title>
                </head>
                <body>
                  <header>
                    <h1>Verification code</h1>
                    <p>It's your verification code</p>
                  </header>
                  <main>
                    <div class="verification-code">
                      {{
                        ForeachCode(code)
                      }}
                    </div>
                    <p class="footer-text">or click below</p>
                  </main>
                  <footer>
                    <a href="{{linkToVerification}}">
                      <div class="verification-btn">
                        verificate
                      </div>
                    <a>
                  </footer>
                </body>
                <style>
                    body {
                      border: 2px solid #F9F9F9;
                      border-top-left-radius: 10px;
                      border-top-right-radius: 10px;
                    }
                    header {
                      padding: 20px;
                      background: #F9F9F9;
                      border-top-left-radius: 10px;
                      border-top-right-radius: 10px;
                      cursor: default;
                    }
                    header h1 {
                      margin: 0;
                      text-align: center;
                      font-family: sans-serif;
                      color: #363537;
                    }
                    header p {
                      margin: 5px 0;
                      text-align: center;
                      font-family: sans-serif;
                      color: #363537;
                    }
                    .verification-code {
                      display: flex;
                      justify-content: center;
                      margin-top: 50px;
                      cursor: text;
                    }
                    .code-cell {
                      display: inline-block;
                      padding: 10px;
                      margin: 0 5px;
                      border: 2px solid #63ABE6;
                      border-radius: 8px;
                      text-align: center;
                    }
                    .code-cell p {
                      display: inline-block;
                      margin: 0;
                      padding: 0;
                      font-size: 23px;
                      font-family: Sans-Serif;
                      font-weight: bold;
                      color: #363537;
                    }
                    .footer-text {
                      padding-top: 50px;
                      font-size: 13px;
                      text-align: center;
                      font-family: Sans-Serif;
                      color: #363537;
                      cursor: default;
                    }
                    footer {
                      display: flex;
                      justify-content: center;
                    }
                    .verification-btn {
                      display: inline-block;
                      padding: 13px 28px;
                      margin-bottom: 50px;
                      color: #3C99E6;
                      border-radius: 25px;
                      border: 2px solid #3C99E6;
                      font-size: 18px;
                      font-family: Sans-Serif;
                      font-weight: bold;
                      cursor: pointer;
                    }
                    .verification-btn:hover {
                      background: #3C99E6;
                      color: white;
                      transition: .5s;
                    }
                </style>
              </html>
            """;
    }

    private static string ForeachCode(string code)
    {
        var codeCellsBuilder = new StringBuilder();
        foreach (var digit in code)
        {
            codeCellsBuilder.Append($"<div class=\"code-cell\"><p>{digit}</p></div>");
        }

        return codeCellsBuilder.ToString();
    }
}