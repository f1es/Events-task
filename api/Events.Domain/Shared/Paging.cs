namespace Events.Domain.Shared;

public record Paging(
    int Page = 1,
    int PageSize = 10);
