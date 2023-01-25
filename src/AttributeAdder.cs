using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using mellite.Utilities;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace mellite {

	class AttributeAdder {
		public static string AddMacCatalystAttributes (string text, string path)
		{
			var defines = new List<string> ();
			defines.Add ("NET");
			defines.Add ("__MACCATALYST__");
			defines.Add ("MACCATALYST");
			defines.Add ("HAS_ACCELERATE");
			defines.Add ("HAS_ACCESSIBILITY");
			defines.Add ("HAS_ACCOUNTS");
			defines.Add ("HAS_ADDRESSBOOK");
			defines.Add ("HAS_ADSERVICES");
			defines.Add ("HAS_ADSUPPORT");
			defines.Add ("HAS_APPCLIP");
			defines.Add ("HAS_APPKIT");
			defines.Add ("HAS_APPTRACKINGTRANSPARENCY");
			defines.Add ("HAS_AUDIOTOOLBOX");
			defines.Add ("HAS_AUDIOUNIT");
			defines.Add ("HAS_AUTHENTICATIONSERVICES");
			defines.Add ("HAS_AUTOMATICASSESSMENTCONFIGURATION");
			defines.Add ("HAS_AVFOUNDATION");
			defines.Add ("HAS_AVKIT");
			defines.Add ("HAS_AVROUTING");
			defines.Add ("HAS_BACKGROUNDASSETS");
			defines.Add ("HAS_BACKGROUNDTASKS");
			defines.Add ("HAS_BUSINESSCHAT");
			defines.Add ("HAS_CALLKIT");
			defines.Add ("HAS_CFNETWORK");
			defines.Add ("HAS_CLASSKIT");
			defines.Add ("HAS_CLOUDKIT");
			defines.Add ("HAS_COMPRESSION");
			defines.Add ("HAS_CONTACTS");
			defines.Add ("HAS_CONTACTSUI");
			defines.Add ("HAS_COREANIMATION");
			defines.Add ("HAS_COREAUDIOKIT");
			defines.Add ("HAS_COREBLUETOOTH");
			defines.Add ("HAS_COREDATA");
			defines.Add ("HAS_COREFOUNDATION");
			defines.Add ("HAS_COREGRAPHICS");
			defines.Add ("HAS_COREHAPTICS");
			defines.Add ("HAS_COREIMAGE");
			defines.Add ("HAS_CORELOCATION");
			defines.Add ("HAS_CORELOCATIONUI");
			defines.Add ("HAS_COREMEDIA");
			defines.Add ("HAS_COREMIDI");
			defines.Add ("HAS_COREML");
			defines.Add ("HAS_COREMOTION");
			defines.Add ("HAS_CORENFC");
			defines.Add ("HAS_CORESPOTLIGHT");
			defines.Add ("HAS_CORETELEPHONY");
			defines.Add ("HAS_CORETEXT");
			defines.Add ("HAS_COREVIDEO");
			defines.Add ("HAS_COREWLAN");
			defines.Add ("HAS_DEVICECHECK");
			defines.Add ("HAS_EVENTKIT");
			defines.Add ("HAS_EVENTKITUI");
			defines.Add ("HAS_EXECUTIONPOLICY");
			defines.Add ("HAS_EXTERNALACCESSORY");
			defines.Add ("HAS_FILEPROVIDER");
			defines.Add ("HAS_FOUNDATION");
			defines.Add ("HAS_GAMECONTROLLER");
			defines.Add ("HAS_GAMEKIT");
			defines.Add ("HAS_GAMEPLAYKIT");
			defines.Add ("HAS_HEALTHKIT");
			defines.Add ("HAS_HEALTHKITUI");
			defines.Add ("HAS_HOMEKIT");
			defines.Add ("HAS_IDENTITYLOOKUP");
			defines.Add ("HAS_IDENTITYLOOKUPUI");
			defines.Add ("HAS_IMAGEIO");
			defines.Add ("HAS_INTENTS");
			defines.Add ("HAS_INTENTSUI");
			defines.Add ("HAS_IOSURFACE");
			defines.Add ("HAS_JAVASCRIPTCORE");
			defines.Add ("HAS_LINKPRESENTATION");
			defines.Add ("HAS_LOCALAUTHENTICATION");
			defines.Add ("HAS_MAPKIT");
			defines.Add ("HAS_MEDIAACCESSIBILITY");
			defines.Add ("HAS_MEDIAPLAYER");
			defines.Add ("HAS_MEDIATOOLBOX");
			defines.Add ("HAS_MESSAGES");
			defines.Add ("HAS_MESSAGEUI");
			defines.Add ("HAS_METAL");
			defines.Add ("HAS_METALKIT");
			defines.Add ("HAS_METALPERFORMANCESHADERS");
			defines.Add ("HAS_METALPERFORMANCESHADERSGRAPH");
			defines.Add ("HAS_METRICKIT");
			defines.Add ("HAS_MLCOMPUTE");
			defines.Add ("HAS_MOBILECORESERVICES");
			defines.Add ("HAS_MODELIO");
			defines.Add ("HAS_MULTIPEERCONNECTIVITY");
			defines.Add ("HAS_NATURALLANGUAGE");
			defines.Add ("HAS_NEARBYINTERACTION");
			defines.Add ("HAS_NETWORK");
			defines.Add ("HAS_NETWORKEXTENSION");
			defines.Add ("HAS_OSLOG");
			defines.Add ("HAS_PASSKIT");
			defines.Add ("HAS_PDFKIT");
			defines.Add ("HAS_PENCILKIT");
			defines.Add ("HAS_PHASE");
			defines.Add ("HAS_PHOTOS");
			defines.Add ("HAS_PHOTOSUI");
			defines.Add ("HAS_PUSHKIT");
			defines.Add ("HAS_QUICKLOOK");
			defines.Add ("HAS_QUICKLOOKTHUMBNAILING");
			defines.Add ("HAS_REPLAYKIT");
			defines.Add ("HAS_SAFARISERVICES");
			defines.Add ("HAS_SCENEKIT");
			defines.Add ("HAS_SCREENTIME");
			defines.Add ("HAS_SECURITY");
			defines.Add ("HAS_SENSORKIT");
			defines.Add ("HAS_SHAREDWITHYOU");
			defines.Add ("HAS_SHAREDWITHYOUCORE");
			defines.Add ("HAS_SHAZAMKIT");
			defines.Add ("HAS_SOCIAL");
			defines.Add ("HAS_SOUNDANALYSIS");
			defines.Add ("HAS_SPEECH");
			defines.Add ("HAS_SPRITEKIT");
			defines.Add ("HAS_STOREKIT");
			defines.Add ("HAS_SYSTEMCONFIGURATION");
			defines.Add ("HAS_THREADNETWORK");
			defines.Add ("HAS_UIKIT");
			defines.Add ("HAS_UNIFORMTYPEIDENTIFIERS");
			defines.Add ("HAS_USERNOTIFICATIONS");
			defines.Add ("HAS_USERNOTIFICATIONSUI");
			defines.Add ("HAS_VIDEOTOOLBOX");
			defines.Add ("HAS_VISION");
			defines.Add ("HAS_VISIONKIT");
			defines.Add ("HAS_WEBKIT");
			defines.Add ("HAS_XKIT");

			CSharpParseOptions options = new CSharpParseOptions (preprocessorSymbols: defines);
			SyntaxTree tree = CSharpSyntaxTree.ParseText (text, options, path: path);

			CompilationUnitSyntax root = tree.GetCompilationUnitRoot ();

			Console.WriteLine ($"Processing {path}");
			root = (CompilationUnitSyntax) root!.Accept (new AttributeAdderVisitor ())!;
			return root!.ToFullString ();
		}
	}


	class AttributeAdderVisitor : CSharpSyntaxRewriter {
		public override SyntaxNode? VisitPropertyDeclaration (PropertyDeclarationSyntax node) => Apply (base.VisitPropertyDeclaration (node));
		public override SyntaxNode? VisitMethodDeclaration (MethodDeclarationSyntax node) => Apply (base.VisitMethodDeclaration (node));
		public override SyntaxNode? VisitEventDeclaration (EventDeclarationSyntax node) => Apply (base.VisitEventDeclaration (node));
		public override SyntaxNode? VisitFieldDeclaration (FieldDeclarationSyntax node) => Apply (base.VisitFieldDeclaration (node));
		public override SyntaxNode? VisitDelegateDeclaration (DelegateDeclarationSyntax node) => Apply (base.VisitDelegateDeclaration (node));
		public override SyntaxNode? VisitEnumMemberDeclaration (EnumMemberDeclarationSyntax node) => Apply (base.VisitEnumMemberDeclaration (node));
		public override SyntaxNode? VisitConstructorDeclaration (ConstructorDeclarationSyntax node) => Apply (base.VisitConstructorDeclaration (node));
		public override SyntaxNode? VisitInterfaceDeclaration (InterfaceDeclarationSyntax node) => Apply (base.VisitInterfaceDeclaration (node));
		public override SyntaxNode? VisitEnumDeclaration (EnumDeclarationSyntax node) => Apply (base.VisitEnumDeclaration (node));
		public override SyntaxNode? VisitStructDeclaration (StructDeclarationSyntax node) => Apply (base.VisitStructDeclaration (node));
		public override SyntaxNode? VisitClassDeclaration (ClassDeclarationSyntax node) => Apply (base.VisitClassDeclaration (node));
		public override SyntaxNode? VisitAccessorDeclaration (AccessorDeclarationSyntax node) => Apply (base.VisitAccessorDeclaration (node));

		SyntaxNode? Apply (SyntaxNode? node)
		{
			if (node is MemberDeclarationSyntax member)
				return Apply (member, node.Parent as MemberDeclarationSyntax);
			if (node is AccessorDeclarationSyntax accessor)
				return Apply (accessor, node.Parent as MemberDeclarationSyntax);
			return node;
		}

		public T Apply<T> (T member, MemberDeclarationSyntax? parent) where T: CSharpSyntaxNode
		{
			//if (member.ToString ().Contains ("PerformAccessorySetup"))
			//	Console.WriteLine ("STOP");
			var indentTrivia = AttributeHarvester.GetIndentTrivia (member);

			var macCatalystIntroducedAttribute = GetMacCatalystIntroducedAttribute (member, indentTrivia, out var _);
			var macCatalystDeprecatedAttribute = GetMacCatalystDeprecatedAttribute (member, indentTrivia);
			if (macCatalystIntroducedAttribute is null && macCatalystDeprecatedAttribute is null)
				return member;

			var location = member.SyntaxTree.GetLineSpan (member.Span).ToString ();
			if (macCatalystIntroducedAttribute is not null)
				Console.WriteLine ($"{location} {member.GetType ().Name} Added: {macCatalystIntroducedAttribute}");
			if (macCatalystDeprecatedAttribute is not null)
				Console.WriteLine ($"{location} {member.GetType ().Name} Deprecated: {macCatalystDeprecatedAttribute}");

			// Figure out where the new attribute(s) are to be added relative to existing attributes
			// * If there's no trailing trivia, it's because of something like this:
			//     [Attrib] Whatever
			//   and in that case we add at the beginning:
			//     [NewAttrib]
			//     [Attrib] Whatever
			// * Otherwise we add after the last availability attribute (to keep the availability attributes together).
			var addIndex = -1;
			var finalAttributes = new List<AttributeListSyntax> ();
			var memberAttributeLists = member.GetAttributeLists ();
			if (memberAttributeLists.Any ()) {
				var trailing = memberAttributeLists.Last ().GetTrailingTrivia ();
				if (!trailing.Any (v => v.Kind () == SyntaxKind.EndOfLineTrivia))
					addIndex = 0;
			}
			if (addIndex == -1) {
				var lastAvailabilityIndex = memberAttributeLists.LastIndexOf (v => v.Attributes.Any (x => {
					var attribName = (x.Name as IdentifierNameSyntax)?.Identifier.ValueText;
					switch (attribName) {
					case "Mac":
					case "iOS":
					case "TV":
					case "Watch":
					case "MacCatalyst":
					case "Introduced":
					case "Deprecated":
					case "NoMac":
					case "NoiOS":
					case "NoTV":
					case "NoMacCatalyst":
					case "NoWatch":
					case "Unavailable":
					case "Obsoleted":
						return true;
					}
					return false;
				}));
				if (lastAvailabilityIndex >= 0)
					addIndex = lastAvailabilityIndex + 1;
			}

			if (addIndex == -1)
				addIndex = memberAttributeLists.Count;

			finalAttributes.AddRange (memberAttributeLists);

			if (macCatalystDeprecatedAttribute is not null)
				finalAttributes.Insert (addIndex, macCatalystDeprecatedAttribute);
			if (macCatalystIntroducedAttribute is not null)
				finalAttributes.Insert (addIndex, macCatalystIntroducedAttribute);

			return member.WithAttributeLists<T> (new SyntaxList<AttributeListSyntax> (finalAttributes));
		}

		AttributeListSyntax CreateMacCatalystAttribute (SyntaxTriviaList indentTrivia, string attributeName, string? platformVersion = null)
		{
			AttributeSyntax newNode;
			if (!string.IsNullOrEmpty (platformVersion)) {
				var args = SyntaxFactory.ParseAttributeArgumentList ("(" + platformVersion + ")");
				newNode = SyntaxFactory.Attribute (SyntaxFactory.ParseName (attributeName), args);
			} else {
				newNode = SyntaxFactory.Attribute (SyntaxFactory.ParseName (attributeName));
			}
			return newNode.ToAttributeList ().WithLeadingTrivia (indentTrivia).WithTrailingTrivia (TriviaConstants.Newline);
		}

		Version GetVersion (IList<AttributeArgumentSyntax> arguments)
		{
			var fields = arguments.Select (v => v.Expression).Cast<LiteralExpressionSyntax> ().Select (v => (int) v.Token.Value!).ToArray ();
			switch (fields.Length) {
			case 2:
				return new Version (fields [0], fields [1]);
			case 3:
				return new Version (fields [0], fields [1], fields [2]);
			default:
				throw new NotImplementedException (fields.Length.ToString ());
			}
		}

		bool TryGetIntroducedAttribute (IEnumerable<AttributeSyntax> allAttributes, string platform, [NotNullWhen (true)] out AttributeSyntax? attribute, [NotNullWhen (true)] out Version? version)
		{
			foreach (var attrib in allAttributes) {
				var name = (attrib.Name as IdentifierNameSyntax)?.Identifier.ValueText;
				if (string.IsNullOrEmpty (name))
					continue;
				if (name == platform) {
					version = GetVersion (attrib.ArgumentList!.Arguments.ToArray ());
				} else if (name == "Introduced") {
					var args = attrib.ArgumentList!.Arguments.ToArray ();
					var pl = args [0];
					if (!pl.ToString ().Contains (platform))
						continue;

					version = GetVersion (args.Skip (1).ToArray ());
				} else {
					continue;
				}
				attribute = attrib;
				return true;
			}

			version = null;
			attribute = null;
			return false;
		}

		bool TryGetDeprecatedAttribute (IEnumerable<AttributeSyntax> allAtributes, string platform, [NotNullWhen (true)] out AttributeSyntax? attribute)
		{
			var introduced = allAtributes.Where (v => (v.Name as IdentifierNameSyntax)?.Identifier.ValueText == "Deprecated");
			foreach (var intr in introduced) {
				var args = intr.ArgumentList!.Arguments.ToArray ();
				var pl = args [0];
				if (!pl.ToString ().Contains (platform))
					continue;

				attribute = intr;
				return true;
			}

			attribute = null;
			return false;
		}

		AttributeListSyntax? GetMacCatalystDeprecatedAttribute (CSharpSyntaxNode member, SyntaxTriviaList indentTrivia)
		{
			var allAttributes = member.GetAllAttributes ();

			// The member already has a MacCatalyst attribute, so don't add anything else
			if (TryGetDeprecatedAttribute (allAttributes, "MacCatalyst", out var _))
				return null;

			// Does the member have an deprecate iOS attribute?
			if (!TryGetDeprecatedAttribute (allAttributes, "iOS", out var iOSAttribute))
				return null;

			var arguments = iOSAttribute.ArgumentList!.Arguments.Select (v => v.ToString ()).ToArray ();
			switch (arguments.Length) {
			case 3:
			case 4:
				arguments [0] = "PlatformName.MacCatalyst";
				var version = Version.Parse (arguments [1] + "." + arguments [2]);
				if (version.Major <= 13)
					version = new Version (13, 1);
				arguments [1] = version.Major.ToString ();
				arguments [2] = version.Minor.ToString ();
				break;
			default:
				throw new NotImplementedException (arguments.Length.ToString ());
			}

			var attributeCode = "(" + string.Join (", ", arguments) + ")";
			var args = SyntaxFactory.ParseAttributeArgumentList (attributeCode);
			var newNode = SyntaxFactory.Attribute (SyntaxFactory.ParseName ("Deprecated"), args);
			var newAttribute = newNode.ToAttributeList ().WithLeadingTrivia (indentTrivia).WithTrailingTrivia (TriviaConstants.Newline);
			return newAttribute;
		}

		bool IsMacCatalystUnavailable (CSharpSyntaxNode? member)
		{
			if (member is null)
				return false;

			if (member is NamespaceDeclarationSyntax)
				return false;

			if (member is AccessorListSyntax)
				return IsMacCatalystUnavailable (member.Parent as CSharpSyntaxNode);

			var allAttributes = member.GetAllAttributes ();
			if (allAttributes.Any (v => (v.Name as IdentifierNameSyntax)?.Identifier.ValueText == "NoMacCatalyst"))
				return true;
			if (allAttributes.Any (v => (v.Name as IdentifierNameSyntax)?.Identifier.ValueText == "Unavailable" && v.ArgumentList?.Arguments.Count == 1 && v.ArgumentList.Arguments [0].ToString ().Contains ("MacCatalyst")))
				return true;

			return IsMacCatalystUnavailable (member.Parent as CSharpSyntaxNode);
		}

		AttributeListSyntax? GetMacCatalystIntroducedAttribute<T> (T? member, SyntaxTriviaList indentTrivia, out Version? macCatalystIntroducedVersion) where T: CSharpSyntaxNode
		{
			macCatalystIntroducedVersion = null;

			if (member is null)
				return null;

			if (member is NamespaceDeclarationSyntax ns)
				return null;

			var allAttributes = member.GetAllAttributes ();

			// If there are no availability attributes at all on the member, don't add a MacCatalyst one
			if (!allAttributes.Any (v => {
				var name = (v.Name as IdentifierNameSyntax)?.Identifier.ValueText;
				return name == "Introduced" || name == "iOS" || name == "MacCatalyst" || name == "TV" || name == "Mac" || name == "Watch" || name == "NoiOS" || name == "NoMacCatalyst" || name == "NoTV" || name == "NoMac" || name == "NoWatch";
			})) {
				return null;
			}

			// The member already has a MacCatalyst attribute, so don't add anything else
			if (TryGetIntroducedAttribute (allAttributes, "MacCatalyst", out var _, out macCatalystIntroducedVersion))
				return null;

			// The member (or a parent) already has a NoMacCatalyst attributes, so don't add anything else
			if (IsMacCatalystUnavailable (member))
				return null;

			// Is iOS unavailable?
			if (allAttributes.Any (v => (v.Name as IdentifierNameSyntax)?.Identifier.ValueText == "NoiOS")) {
				// iOS is not available! In that case, it's implied that MacCatalyst isn't available either.
				return CreateMacCatalystAttribute (indentTrivia, "NoMacCatalyst");
			}

			// Get the parent's attribute
			var parentIntroducedAttribute = GetMacCatalystIntroducedAttribute (member.Parent as MemberDeclarationSyntax, indentTrivia, out var parentIntroducedVersion);

			// Is iOS available?
			if (!TryGetIntroducedAttribute (allAttributes, "iOS", out var iOSAttribute, out var introducedVersion)) {
				// An iOS attribute is not available, so return the parent attribute, if present
				if (parentIntroducedAttribute is not null)
					return parentIntroducedAttribute;
				// No parent attribute, so return the default MacCatalyst attribute
				return CreateMacCatalystAttribute (indentTrivia, "MacCatalyst", "13, 1");
			}

			// We have an iOS attribute
			string platformVersion;
			if (parentIntroducedVersion is not null && parentIntroducedVersion > introducedVersion) {
				// If we have a parent attribute, and that parent's attribute's version is higher than the iOS attribute's version, then use the parent's attribute
				platformVersion = parentIntroducedVersion.ToString ().Replace (".", ", ");
			} else if (introducedVersion.Major > 13) {
				// if the iOS version > 13, then use that for the MacCatalyst version
				platformVersion = introducedVersion.ToString ().Replace (".", ", ");
			} else {
				// otherwise use MacCatalyst 13.1, which is the first version we support.
				platformVersion = "13, 1";
			}
			return CreateMacCatalystAttribute (indentTrivia, "MacCatalyst", platformVersion);
		}
	}

	static class SyntaxExtensions {
		public static IEnumerable<AttributeSyntax> GetAllAttributes (this CSharpSyntaxNode self)
		{
			SyntaxList<AttributeListSyntax> attributes;

			if (self is MemberDeclarationSyntax member) {
				attributes = member.AttributeLists;
			} else if (self is AccessorDeclarationSyntax accessor) {
				attributes = accessor.AttributeLists;
			} else {
				throw new NotImplementedException (self.GetType ().FullName);
			}
			return attributes.SelectMany (x => x.Attributes);
		}

		public static SyntaxList<AttributeListSyntax> GetAttributeLists (this CSharpSyntaxNode self)
		{
			SyntaxList<AttributeListSyntax> attributes;

			if (self is MemberDeclarationSyntax member) {
				attributes = member.AttributeLists;
			} else if (self is AccessorDeclarationSyntax accessor) {
				attributes = accessor.AttributeLists;
			} else {
				throw new NotImplementedException (self.GetType ().FullName);
			}
			return attributes;
		}

		public static T WithAttributeLists<T> (this CSharpSyntaxNode self, SyntaxList<AttributeListSyntax> attributeLists) where T: CSharpSyntaxNode
		{
			if (self is MemberDeclarationSyntax member) {
				return (T) (object) member.WithAttributeLists (attributeLists);
			} else if (self is AccessorDeclarationSyntax accessor) {
				return (T) (object) accessor.WithAttributeLists (attributeLists);
			} else {
				throw new NotImplementedException (self.GetType ().FullName);
			}
		}
	}
}
