namespace Application.Configuration;

public class AppSettings
{
	public bool AllowShowRequirement { get; set; }
	public bool RequierdBPMSCode { get; set; }
	public string DashboardItems { get; set; }
	public int RateLimitSeconds { get; set; }
	public int RateLimitAllowRequest { get; set; }
	public bool HasHoverContextMenu { get; set; }
	public bool ShowErrorMessage { get; set; }
	public bool ValidateContentType { get; set; }
	public bool TreeViewExpandAll { get; set; }
	public bool WorkFlowAllowOneUserAccept { get; set; }
	public string PanelTheme { get; set; }
	public bool FailedLoginLock { get; set; }
	public bool ShowIndicatorRealScoreNotif { get; set; }
	public bool ALU_HASIB_CHK { get; set; }
	public string PowerBIReportAddress { get; set; }
	public string PowerBIReportUserName { get; set; }
	public string PowerBIReportUserPassword { get; set; }
	public string WebType { get; set; }
	public string MasterDomain { get; set; }
	public int StrategyType { get; set; }
	public string SSO_Redirect_Uri { get; set; }
	public string RunMode { get; set; }
	public string SSOType { get; set; }
	public string Company { get; set; }
	public string VersionType { get; set; }
	public string ReportUrl { get; set; }
	public string Access { get; set; }
	public string Direction { get; set; }
	public string CalendarType { get; set; }
}