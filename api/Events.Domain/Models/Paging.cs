namespace Events.Domain.Models;

public record Paging(
    int Page = 1,
    int PageSize = 10);
