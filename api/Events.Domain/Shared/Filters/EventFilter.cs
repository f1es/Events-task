namespace Events.Domain.Shared.Filters;

public record EventFilter(
	string? Search,
	string? SortItem,
	string? SortOrder);