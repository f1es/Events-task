namespace Events.Domain.Models;

public record EventFilter(
    string? Search,
    string? SortItem,
    string? SortOrder);