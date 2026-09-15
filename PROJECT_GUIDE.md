# מדריך קבצים — checkBack

## קבצי שורש

| קובץ | תיאור |
|------|--------|
| `Program.cs` | נקודת הכניסה לשרת. מגדיר את סדר ה-Middleware, CORS, JWT ורישום השירותים |
| `appsettings.json` | הגדרות בסיס — ללא סודות. מכיל את מבנה ה-JWT עם ערכים ריקים |
| `appsettings.Development.json` | הגדרות פיתוח מקומי — כולל מפתח ה-JWT האמיתי |
| `checkBack.csproj` | קובץ פרויקט .NET — מגדיר חבילות NuGet ותצורת build |

---

## Controllers — נקודות קצה

| קובץ | תיאור |
|------|--------|
| `Controllers/AuthController.cs` | מטפל ב-`POST /api/auth/login`. מחזיר JWT + שם משתמש + תפקיד |
| `Controllers/TasksController.cs` | מטפל ב-`GET /api/tasks`, `PATCH /api/tasks/{id}`, ו-`GET /api/tasks/admin` (אדמין בלבד) |

---

## DTOs — מודלי העברת נתונים

| קובץ | תיאור |
|------|--------|
| `DTOs/Requests/LoginRequestDto.cs` | מבנה הבקשה לכניסה — שם משתמש וסיסמה עם ולידציה |
| `DTOs/Responses/LoginResponseDto.cs` | מבנה התשובה לכניסה — Token, שם משתמש, תפקיד |
| `DTOs/Responses/TaskDto.cs` | מבנה משימה בודדת שנשלחת ל-client |
| `DTOs/Responses/UserTasksDto.cs` | מבנה רשימת משימות לפי משתמש — לשימוש תצוגת אדמין |

---

## Services — לוגיקה עסקית

| קובץ | תיאור |
|------|--------|
| `Services/Interfaces/IAuthService.cs` | ממשק לשירות אימות — מגדיר `Authenticate()` |
| `Services/Interfaces/IJwtService.cs` | ממשק לשירות JWT — מגדיר `GenerateToken()` |
| `Services/Interfaces/ITaskService.cs` | ממשק לשירות משימות — מגדיר `GetTasks`, `ToggleTask`, `GetAllUserTasks` |
| `Services/AuthService.cs` | מאמת שם משתמש וסיסמה מול מילון משתמשים בזיכרון |
| `Services/JwtService.cs` | מייצר טוקן JWT עם claims: שם, תפקיד, מזהה ייחודי |
| `Services/TaskService.cs` | מנהל משימות בזיכרון עבור כל משתמש. **Singleton** — שומר מצב בין בקשות |

---

## Common, Models, Middleware, Extensions

| קובץ | תיאור |
|------|--------|
| `Common/ApiResponse.cs` | עטיפה אחידה לכל תשובות ה-API: `{ success, message, data }` |
| `Models/TaskItem.cs` | מודל משימה פנימי בזיכרון — Id, Title, Completed |
| `Middleware/GlobalExceptionMiddleware.cs` | תופס שגיאות לא מטופלות ומחזיר 500 עם הודעה גנרית |
| `Extensions/ServiceExtensions.cs` | רישום כל ה-Services להזרקת תלויות (DI) |
| `Extensions/AuthExtensions.cs` | הגדרת אימות JWT Bearer מתוך ה-config |
