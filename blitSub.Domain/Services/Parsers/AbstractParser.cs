using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using blitSub.Domain.Models;
using blitSub.Domain.Util;

namespace blitSub.Domain.Services.Parsers
{
    public class AbstractParser
    {
        private static readonly string TAG = AbstractParser.getName();
        private static readonly string SUBSONIC_RESPONSE = "subsonic-response";
	    private static readonly string MADSONIC_RESPONSE = "madsonic-response";
	    private static readonly string SUBSONIC = "subsonic";
	    private static readonly string MADSONIC = "madsonic";
	    private static readonly string AMPACHE = "ampache";

        protected readonly Context context;
	    protected readonly int instance;
        private XmlPullParser parser;
        private bool rootElementFound;

        public AbstractParser(Context context, int instance)
        {
            this.context = context;
            this.instance = instance;
        }

        protected Context getContext()
        {
            return context;
        }

        protected void handleError()
        {
            int code = getInteger("code");
            string message;
            switch (code) {
			    case 0:
				    message = context.getResources().getString(R.string.parser_server_error, get("message"));
				    break;
                case 20:
                    message = context.getResources().getString(R.string.parser_upgrade_client);
                    break;
                case 30:
                    message = context.getResources().getString(R.string.parser_upgrade_server);
                    break;
                case 40:
                    message = context.getResources().getString(R.string.parser_not_authenticated);
                    break;
			    case 41:
				    Util.setBlockTokenUse(context, instance, true);

				    // Throw IOException so RESTMusicService knows to retry
				    throw new IOException();
                case 50:
                    message = context.getResources().getString(R.string.parser_not_authorized);
                    break;
                default:
                    message = get("message");
                    break;
        }
        throw new SubsonicRESTException(code, message);
}

protected void updateProgress(ProgressListener progressListener, int messageId)
{
    if (progressListener != null)
    {
        progressListener.updateProgress(messageId);
    }
}

protected void updateProgress(ProgressListener progressListener, string message)
{
    if (progressListener != null)
    {
        progressListener.updateProgress(message);
    }
}

protected string getText()
{
    return parser.getText();
}

protected string get(string name)
{
    return parser.getAttributeValue(null, name);
}

protected bool getBoolean(string name)
{
    return "true".Equals(get(name));
}

protected int getInteger(string name)
{
    string s = get(name);
    try
    {
        return (s == null || "".Equals(s)) ? null : int.Parse(s);
    }
    catch (Exception e)
    {
        Log.w(TAG, "Failed to parse " + s + " into integer");
        return null;
    }
}

protected long getLong(string name)
{
    string s = get(name);
    return s == null ? null : long.Parse(s);
}

protected float getFloat(string name)
{
    string s = get(name);
    return s == null ? null : float.Parse(s);
}

protected void init(Reader reader)
{
    parser = Xml.newPullParser();
    parser.setInput(reader);
    rootElementFound = false;
}

protected int nextParseEvent()
{
		try {
        return parser.next();
    } catch(Exception e) {
        if (ServerInfo.isMadsonic6(context, instance))
        {
            ServerInfo overrideInfo = new ServerInfo();
            overrideInfo.saveServerInfo(context, instance);
        }

        throw e;
    }
}

protected string getElementName()
{
    string name = parser.getName();
    if (SUBSONIC_RESPONSE.Equals(name) || MADSONIC_RESPONSE.Equals(name))
    {
        rootElementFound = true;
        string version = get("version");
        if (version != null)
        {
            ServerInfo server = new ServerInfo();
            server.setRestVersion(new Models.Version(version));

            if (MADSONIC.Equals(get("type")) || MADSONIC_RESPONSE.Equals(name))
            {
                server.setRestType(ServerInfo.TYPE_MADSONIC);
            }
            if (AMPACHE.Equals(get("type")))
            {
                server.setRestType(ServerInfo.TYPE_AMPACHE);
            }
            else if (SUBSONIC.Equals(get("type")) && server.checkServerVersion(context, "1.13"))
            {
                // Oh am I going to regret this
                server.setRestType(ServerInfo.TYPE_MADSONIC);
                server.setRestVersion(new Models.Version("2.0.0"));
            }
            server.saveServerInfo(context, instance);
        }
    }
    return name;
}

protected void validate()
{
        if (!rootElementFound) {
        if (ServerInfo.isMadsonic6(context, instance))
        {
            ServerInfo overrideInfo = new ServerInfo();
            overrideInfo.saveServerInfo(context, instance);
        }

        throw new Exception(context.getResources().getString(R.string.background_task_parse_error));
    }
}
    }
}
