# Quiz Application

## Task Description
Create a web application to solve quiz entries and display high scores from previous results.

## Functional Requirements

### Pages
The application should contain two main pages:
1. **Quiz Page** - For solving the quiz.
2. **High Score Page** - For displaying high scores.

### Quiz Questions
The quiz should contain 10 questions. The questions should be of 3 different types:
- **Radio buttons** (single answer)
- **Checkbox** (multiple answers)
- **Textbox** (manual text input)

Example:
- 4 **single answer** questions (radio buttons).
- 2 **text input** questions (textbox).
- 4 **multiple answer** questions (checkbox).

### Email
When solving the quiz, the user must enter their **email**, which will be saved alongside their score.

### High Score Page
- Only the **top 10 scores** are displayed.
- Each entry should display:
  - Position
  - Email
  - Score
  - Date and time of submission
- **Top 3 placements** should be highlighted with gold, silver, and bronze colors.

### Calculation Rules
- **Radio buttons**: If the answer is correct, the user gets **+100 points**.
- **Checkboxes**: Points are awarded based on the formula:
  - `(100 / number of correct answers) * number of correct checks`
  - No decimal points, rounded up.
- **Textbox**: Points are awarded only if the answer exactly matches the correct answer (case insensitive). **+100 points** for a correct answer.

---

## Technical Requirements

- **Backend**: 
  - Built using **ASP.NET Core**.
  - **EF Core** in-memory database to store quiz entries, answers, and high scores.
  
- **Frontend**: 
  - Built using **React**.
  - The frontend can use a modern UI library such as **Bootstrap**, **Material UI**, **Tailwind**, etc.

- **Backend Logic**: 
  - All calculation logic must be implemented on the **backend**.

- **GitHub**: 
  - Code must be shared via GitHub.
  
- **Authentication**: 
  - No authentication is needed.

---

## Nice to Have (Optional)

- **Unit tests** in the backend.
- **Dependency injection**.
- **Mapper package** or custom solution.
- **React Stepper component** for the Quiz Solving page.
- Use of **TypeScript** for both frontend and backend.

---

## Evaluation Criteria

The project will be evaluated based on the following:

- **Functionality** of the web app (it is not required to be 100% finished).
- **Clean code** practices.
- Decisions made regarding the **architecture** for both the backend and frontend.
- **Tools used** to build the application.
- Use of appropriate **design patterns**.

---

## Company Information

- **Company Name**: Uždaroji akcinė bendrovė / A limited liability company „PRESENT CONNECTION“
- **Company No**: 302856551
- **VAT No**: LT100007833614
- **Address**: Jonavos g. 7, Kaunas, LT44191, Lietuva

---
