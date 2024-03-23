# Test task for a developer: Flight search API aggregator

## Goal:
Develop an API that aggregates information about available flights from various sources and provides this information upon client request.

## Tasks:
1. **Develop an API for aggregating flight data.**
   - The API should be able to process requests to search for flights based on criteria (from, to, date - additional parameters can be invented), aggregate data from different sources (at least 2 fictional test sources), and return them in a unified format. The main emphasis should be on ensuring error resilience and providing partially incoming information when some sources are unavailable or have low performance. Additionally, the ability to add new sources without significant changes to the code should be provided.

2. **Booking selected flights.**
   - The API should provide the ability to book the selected flight by sending a corresponding booking request to the source from which the flight was obtained. The request format can be devised at your discretion.

3. **Caching queries.**
   - Implement a caching mechanism for frequently requested queries to reduce server load and shorten response time.

4. **Logging requests.**
   - All API requests should be logged for subsequent analysis.

5. **Provide the ability to handle unpredictably long responses from sources correctly.**

## Technical requirements:
- **Programming language:** .NET (6.0 and above)
- **API documentation** with examples of requests and responses (preferably in OpenAPI format if REST is chosen)
- The **code must be covered by tests** (at least a few for example)

## Additional tasks (optional):
- Implementation of **authentication and authorization** for API access.
- Deploying the API on any **cloud service** (AWS, Azure).
- Creating a simple **client application** (web or console), demonstrating how your API works.

## Results:
- Source code of the application on **GitHub or another public repository**.
- A **brief description** of the task and architecture in the **Readme** file.
- *(Optional)* Link to the **working API in the cloud** and/or **demonstration** of the client application.
