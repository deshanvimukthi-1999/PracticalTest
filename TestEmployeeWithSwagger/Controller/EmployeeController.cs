using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TestEmployeeWithSwagger.Models;

namespace TestEmployeeWithSwagger.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly HttpClient httpClient;
        private readonly string baseUri = "http://examination.24x7retail.com";
        private readonly string apiKey = "?D(G+KbPeSgVkYp3s6v9y$B&E)H@McQf";

        public EmployeeController()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var response = await httpClient.GetAsync($"{baseUri}/api/v1.0/Employees");

                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"Error retrieving employees: {responseContent}");
                }

                var employeesJson = await response.Content.ReadAsStringAsync();
                return Ok(employeesJson);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"An error occurred while retrieving employees: {ex.Message}");
            }
        }


        [HttpGet("{empNo}")]
        public async Task<IActionResult> GetEmployeeByEmpNo(string empNo)
        {
            try
            {
                var response = await httpClient.GetAsync($"{baseUri}/api/v1.0/Employee/{empNo}");
                response.EnsureSuccessStatusCode();
                var employeeJson = await response.Content.ReadAsStringAsync();
                return Ok(employeeJson);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"An error occurred while retrieving employee {empNo}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEmployee(Employee employee)
        {
            try
            {
                var employeeJson = JsonSerializer.Serialize(employee);
                var content = new StringContent(employeeJson, System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{baseUri}/api/v1.0/Employee", content);

                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"Error adding a new employee: {responseContent}");
                }

                var result = await response.Content.ReadAsStringAsync();
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"An error occurred while adding a new employee: {ex.Message}");
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            try
            {
                var employeeJson = JsonSerializer.Serialize(employee);
                var content = new StringContent(employeeJson, System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"{baseUri}/api/v1.0/Employee", content);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"An error occurred while updating the employee: {ex.Message}");
            }
        }

        [HttpDelete("{empNo}")]
        public async Task<IActionResult> DeleteEmployee(string empNo)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{baseUri}/api/v1.0/Employee/{empNo}");
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"An error occurred while deleting employee {empNo}: {ex.Message}");
            }
        }
    }
}
