# HR Management Solution - Employee Management System

A full-stack Employee Management solution built with ASP.NET Core Web API (backend) and Angular (frontend).

## Features

- **Employee CRUD Operations**: Create, Read, Update, and Delete employee records
- **Employee List View**: Display all employees in a responsive table with actions
- **Employee Detail View**: View detailed information about individual employees
- **Employee Form**: Create and edit employees with form validation
- **RESTful API**: Backend API with proper error handling and validation
- **Responsive UI**: Clean and modern user interface

## Technology Stack

### Backend
- ASP.NET Core 10.0 Web API
- In-memory data storage
- CORS enabled for frontend communication
- Data validation with DataAnnotations

### Frontend
- Angular 19
- Reactive Forms for form handling
- HttpClient for API communication
- Standalone components architecture
- CSS for styling

## Project Structure

```
Hr-management-solution/
├── HrManagementApi/          # Backend API
│   ├── Controllers/
│   │   └── EmployeesController.cs
│   ├── Models/
│   │   └── Employee.cs
│   └── Program.cs
│
└── HrManagementFrontend/     # Frontend Angular App
    └── src/
        └── app/
            ├── employee/
            │   ├── employee-list/
            │   ├── employee-detail/
            │   ├── employee-form/
            │   ├── models/
            │   └── employee.service.ts
            └── app.routes.ts
```

## Getting Started

### Prerequisites

- .NET 10.0 SDK
- Node.js (v18 or higher)
- npm or yarn

### Backend Setup

1. Navigate to the backend directory:
```bash
cd HrManagementApi
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Run the API:
```bash
dotnet run --urls "http://localhost:5000"
```

The API will be available at `http://localhost:5000`

### Frontend Setup

1. Navigate to the frontend directory:
```bash
cd HrManagementFrontend
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
npm start
```

The application will be available at `http://localhost:4200`

## API Endpoints

- `GET /api/employees` - Get all employees
- `GET /api/employees/{id}` - Get employee by ID
- `POST /api/employees` - Create new employee
- `PUT /api/employees/{id}` - Update employee
- `DELETE /api/employees/{id}` - Delete employee

## Sample Data

The application comes pre-loaded with sample employee data:

1. John Doe - Software Engineer (IT)
2. Jane Smith - HR Manager (HR)
3. Michael Johnson - Financial Analyst (Finance)

## Employee Model

```json
{
  "id": 1,
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@company.com",
  "phone": "+1234567890",
  "department": "IT",
  "position": "Software Engineer",
  "hireDate": "2020-01-15",
  "salary": 75000
}
```

## Features in Detail

### Employee List
- View all employees in a sortable table
- Actions: View, Edit, Delete for each employee
- Add new employee button
- Real-time data updates

### Employee Detail
- View complete employee information
- Options to edit or delete
- Navigate back to list

### Employee Form
- Create new employees
- Edit existing employees
- Form validation with error messages
- Required fields: First Name, Last Name, Email, Department, Position, Hire Date, Salary
- Optional fields: Phone

### Data Validation

#### Backend Validation
- Required fields validation
- Email format validation
- Phone number format validation
- String length constraints
- Salary range validation

#### Frontend Validation
- Required field indicators
- Email format validation
- Real-time validation feedback
- Submit button disabled during save

## Development Notes

### CORS Configuration
The backend is configured to accept requests from `http://localhost:4200` (Angular dev server).

### Change Detection
The frontend uses manual change detection triggers (`ChangeDetectorRef`) to ensure proper view updates in Angular standalone components.

### Data Storage
Currently uses in-memory storage. Data will be reset when the API restarts.

## Future Enhancements

- Database integration (SQL Server/PostgreSQL)
- Authentication and authorization
- Advanced search and filtering
- Pagination for large datasets
- Employee photo upload
- Export to CSV/Excel
- Department and position management
- Performance reviews module
- Leave management
- Time tracking

## License

This project is part of an HR Management Solution suite.
